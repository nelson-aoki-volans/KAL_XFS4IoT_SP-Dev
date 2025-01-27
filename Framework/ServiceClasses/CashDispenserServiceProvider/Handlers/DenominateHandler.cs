/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2022
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
\***********************************************************************************************/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Text.RegularExpressions;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CashDispenser.Commands;
using XFS4IoT.CashDispenser.Completions;
using XFS4IoT.Completions;
using XFS4IoTFramework.Storage;

namespace XFS4IoTFramework.CashDispenser
{
    public partial class DenominateHandler
    {
        private Task<DenominateCompletion.PayloadData> HandleDenominate(IDenominateEvents events, DenominateCommand denominate, CancellationToken cancel)
        {
            if (!string.IsNullOrEmpty(denominate.Payload.Mix) &&
                CashDispenser.GetMix(denominate.Payload.Mix) is null)
            {
                return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode, 
                                                                            $"Invalid MixNumber specified. {denominate.Payload.Mix}",
                                                                            DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidMixNumber));
            }

            if (denominate.Payload.Denomination.Currencies is null &&
                denominate.Payload.Denomination.Values is null)
            {
                return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                            $"Invalid amounts and values specified. either amount or values dispensing from each cash units required.",
                                                                            DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination));
            }

            if (denominate.Payload.Denomination.Currencies.Select(c => string.IsNullOrEmpty(c.Key) || Regex.IsMatch(c.Key, "^[A-Z]{3}$")).ToList().Count == 0)
            {
                return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                            $"Invalid currency specified.",
                                                                            DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency));
            }

            double totalAmount = 0;
            if (denominate.Payload.Denomination.Currencies is not null)
                totalAmount= denominate.Payload.Denomination.Currencies.Select(c => c.Value).Sum();


            Denominate denomToDispense = new(denominate.Payload.Denomination.Currencies, denominate.Payload.Denomination.Values, Logger);

            ////////////////////////////////////////////////////////////////////////////
            // 1) Check that a given denomination can currently be paid out or Test that a given amount matches a given denomination.
            if (string.IsNullOrEmpty(denominate.Payload.Mix))
            {
                if (totalAmount == 0 &&
                    (denominate.Payload.Denomination.Values is null ||
                     denominate.Payload.Denomination.Values.Count == 0))
                {
                    return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                "No counts specified to dispense items from the cash units."));
                }

                Denominate.DispensableResultEnum Result = denomToDispense.IsDispensable(Storage.CashUnits);
                switch (Result)
                {
                    case Denominate.DispensableResultEnum.Good:
                        break;
                    case Denominate.DispensableResultEnum.CashUnitError:
                        {
                            return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                        $"Invalid Cash Unit specified to dispense.",
                                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.CashUnitError));
                        }
                    case Denominate.DispensableResultEnum.CashUnitLocked:
                        {
                            return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                        $"Cash unit is locked.",
                                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.CashUnitError));
                        }
                    case Denominate.DispensableResultEnum.CashUnitNotEnough:
                        {
                            return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                        $"Cash unit doesn't have enough notes to dispense.",
                                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.TooManyItems));
                        }
                    case Denominate.DispensableResultEnum.InvalidCurrency:
                        {
                            return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                        $"Invalid currency specified. ",
                                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidCurrency));
                        }
                    case Denominate.DispensableResultEnum.InvalidDenomination:
                        {
                            return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                        $"Invalid denomination specified. ",
                                                                                        DenominateCompletion.PayloadData.ErrorCodeEnum.InvalidDenomination));
                        }
                    default:
                        Contracts.Assert(Result == Denominate.DispensableResultEnum.Good, $"Unexpected result received after an internal IsDispense call. {Result}");
                        break;
                }

                if (denomToDispense.Values is null)
                {
                    return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                $"Mix failed to denominate. {denominate.Payload.Mix}, {denomToDispense.CurrencyAmounts}",
                                                                                DenominateCompletion.PayloadData.ErrorCodeEnum.NotDispensable));
                }
            }
            ////////////////////////////////////////////////////////////////////////////
            //  2) Calculate the denomination, given an amount and mix number.
            else if (!string.IsNullOrEmpty(denominate.Payload.Mix) &&
                     (denominate.Payload.Denomination.Values is null ||
                      denominate.Payload.Denomination.Values.Count == 0))
            {
                if (totalAmount == 0)
                {
                    return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                                $"Specified amount is zero to dispense, but number of notes from each cash unit is not specified as well."));
                }

                denomToDispense.Denomination = CashDispenser.GetMix(denominate.Payload.Mix).Calculate(denomToDispense.CurrencyAmounts, Storage.CashUnits, Common.CashDispenserCapabilities.MaxDispenseItems);

                if (denomToDispense.Values is null)
                {
                    return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                $"Mix failed to denominate. {denominate.Payload.Mix}, {denomToDispense.CurrencyAmounts}",
                                                                                DenominateCompletion.PayloadData.ErrorCodeEnum.NotDispensable));
                }
            }
            ////////////////////////////////////////////////////////////////////////////
            //  3) Complete a partially specified denomination for a given amount.
            else if (!string.IsNullOrEmpty(denominate.Payload.Mix) &&
                     denominate.Payload.Denomination.Values.Count != 0)
            {
                if (totalAmount == 0)
                {
                    return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.InvalidData,
                                                                                $"Specified amount is zero to dispense, but number of notes from each cash unit is not specified as well."));
                }

                Denomination mixDenom = CashDispenser.GetMix(denominate.Payload.Mix).Calculate(denomToDispense.CurrencyAmounts, Storage.CashUnits, Common.CashDispenserCapabilities.MaxDispenseItems);
                if (!mixDenom.Values.OrderBy((denom) => denom.Key).SequenceEqual(denomToDispense.Values.OrderBy((denom) => denom.Key)))
                {
                    return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.CommandErrorCode,
                                                                                $"Specified counts each cash unit to be dispensed is different from the result of mix algorithm. internal mix result " + string.Join(", ", mixDenom.Values.Select(d => d.Key + ":" + d.Value)),
                                                                                DenominateCompletion.PayloadData.ErrorCodeEnum.NotDispensable));
                }
            }
            else
            {
                Contracts.Assert(false, $"Unreachable code.");
            }

            return Task.FromResult(new DenominateCompletion.PayloadData(MessagePayload.CompletionCodeEnum.Success,
                                                                        null,
                                                                        denomToDispense.Values is null ? DenominateCompletion.PayloadData.ErrorCodeEnum.NotDispensable : null,
                                                                        denomToDispense.CurrencyAmounts,
                                                                        denomToDispense.Values,
                                                                        denominate.Payload.Denomination.CashBox));
        }

        private IStorageService Storage { get => Provider.IsA<IStorageService>(); }
    }
}
