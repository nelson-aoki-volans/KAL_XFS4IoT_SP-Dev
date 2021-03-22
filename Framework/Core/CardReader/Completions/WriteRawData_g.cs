/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * WriteRawData_g.cs uses automatically generated parts. 
 * created at 3/18/2021 2:05:35 PM
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.CardReader.Completions
{
    [DataContract]
    [Completion(Name = "CardReader.WriteRawData")]
    public sealed class WriteRawDataCompletion : Completion<WriteRawDataCompletion.PayloadData>
    {
        public WriteRawDataCompletion(string RequestId, WriteRawDataCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {
            public enum ErrorCodeEnum
            {
                MediaJam,
                ShutterFail,
                NoMedia,
                InvalidMedia,
                WriteMethod,
                CardTooShort,
                CardTooLong,
            }


            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null)
                : base(CompletionCode, ErrorDescription)
            {
                ErrorDescription.IsNotNullOrWhitespace($"Null or an empty value for {nameof(ErrorDescription)} in received {nameof(WriteRawDataCompletion.PayloadData)}");

                this.ErrorCode = ErrorCode;
            }

            /// <summary>
            ///Specifies the error code if applicable. The following values are possible:**mediaJam**
            ////The card is jammed. Operator intervention is required.**shutterFail**
            ////The open of the shutter failed due to manipulation or hardware error. Operator intervention is required.**noMedia**
            ////The card was removed before completion of the write action (the event [CardReader.MediaInsertedEvent](#message-CardReader.MediaInsertedEvent) has been generated). For motor driven devices, the write is disabled; i.e. another command has to be issued to enable the reader for card entry.**invalidMedia**
            ////No track found; card may have been inserted or pulled through the wrong way.**writeMethod**
            ////The fwWriteMethod value is inconsistent with device capabilities.**cardTooShort**
            ////The card that was inserted is too short. When this error occurs the card remains at the exit slot.**cardTooLong**
            ////The card that was inserted is too long. When this error occurs the card remains at the exit slot.
            /// </summary>
            [DataMember(Name = "errorCode")] 
            public ErrorCodeEnum? ErrorCode { get; private set; }

        }
    }
}