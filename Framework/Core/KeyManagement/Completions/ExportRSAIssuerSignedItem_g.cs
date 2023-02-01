/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT KeyManagement interface.
 * ExportRSAIssuerSignedItem_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Completions;

namespace XFS4IoT.KeyManagement.Completions
{
    [DataContract]
    [Completion(Name = "KeyManagement.ExportRSAIssuerSignedItem")]
    public sealed class ExportRSAIssuerSignedItemCompletion : Completion<ExportRSAIssuerSignedItemCompletion.PayloadData>
    {
        public ExportRSAIssuerSignedItemCompletion(int RequestId, ExportRSAIssuerSignedItemCompletion.PayloadData Payload)
            : base(RequestId, Payload)
        { }

        [DataContract]
        public sealed class PayloadData : MessagePayload
        {

            public PayloadData(CompletionCodeEnum CompletionCode, string ErrorDescription, ErrorCodeEnum? ErrorCode = null, List<byte> Value = null, RSASignatureAlgorithmEnum? RsaSignatureAlgorithm = null, List<byte> Signature = null)
                : base(CompletionCode, ErrorDescription)
            {
                this.ErrorCode = ErrorCode;
                this.Value = Value;
                this.RsaSignatureAlgorithm = RsaSignatureAlgorithm;
                this.Signature = Signature;
            }

            public enum ErrorCodeEnum
            {
                NoRSAKeyPair,
                AccessDenied,
                KeyNotFound
            }

            /// <summary>
            /// Specifies the error code if applicable. The following values are possible:
            /// * ```noRSAKeyPair``` - The device does not have a private key.
            /// * ```accessDenied``` - The device is either not initialized or not ready for any vendor specific reason.
            /// * ```keyNotFound``` - The data item identified by name was not found.
            /// </summary>
            [DataMember(Name = "errorCode")]
            public ErrorCodeEnum? ErrorCode { get; init; }

            /// <summary>
            /// If a public key was requested then value contains the PKCS#1 formatted RSA public key represented in
            /// DER encoded ASN.1 format. If the security item was requested then value contains the device's Security
            /// Item, which may be vendor specific.
            /// <example>aXRlbSBkYXRhIHJlcXVl ...</example>
            /// </summary>
            [DataMember(Name = "value")]
            [DataTypes(Pattern = @"^[A-Za-z0-9+/]+={0,2}$")]
            public List<byte> Value { get; init; }

            /// <summary>
            /// Specifies the algorithm, used to generate the Signature returned in signature, as one of the following:
            /// 
            /// * ```na``` - No signature algorithm used, no signature will be provided in signature, the data item may 
            ///   still be exported.  
            /// * ```rsassaPkcs1V15``` - RSASSA-PKCS1-v1.5 algorithm used.  
            /// * ```rsassaPss``` - RSASSA-PSS algorithm used.
            /// </summary>
            [DataMember(Name = "rsaSignatureAlgorithm")]
            public RSASignatureAlgorithmEnum? RsaSignatureAlgorithm { get; init; }

            /// <summary>
            /// The RSA signature of the data item exported.
            /// 
            /// This should be omitted when the key signature is not supported. 
            /// <example>U2lnbmF0dXJlIGRhdGE=</example>
            /// </summary>
            [DataMember(Name = "signature")]
            [DataTypes(Pattern = @"^[A-Za-z0-9+/]+={0,2}$")]
            public List<byte> Signature { get; init; }

        }
    }
}
