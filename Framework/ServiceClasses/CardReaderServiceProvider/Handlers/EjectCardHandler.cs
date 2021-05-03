/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
\***********************************************************************************************/

using System.Threading.Tasks;
using System.Threading;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;

namespace XFS4IoTFramework.CardReader
{
    public partial class EjectCardHandler
    {
        private async Task<EjectCardCompletion.PayloadData> HandleEjectCard(IEjectCardEvents events, EjectCardCommand ejectCard, CancellationToken cancel)
        {
            Logger.Log(Constants.DeviceClass, "CardReaderDev.EjectCardAsync()");
            var result = await Device.EjectCardAsync(new EjectCardRequest(ejectCard.Payload.EjectPosition),
                                                     cancel);
            Logger.Log(Constants.DeviceClass, $"CardReaderDev.EjectCardAsync() -> {result.CompletionCode}");

            return new EjectCardCompletion.PayloadData(result.CompletionCode,
                                                       result.ErrorDescription,
                                                       result.ErrorCode);
        }

    }
}
