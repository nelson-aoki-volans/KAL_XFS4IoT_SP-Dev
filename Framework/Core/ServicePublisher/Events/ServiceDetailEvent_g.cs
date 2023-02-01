/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT ServicePublisher interface.
 * ServiceDetailEvent_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using XFS4IoT.Events;

namespace XFS4IoT.ServicePublisher.Events
{

    [DataContract]
    [Event(Name = "ServicePublisher.ServiceDetailEvent")]
    public sealed class ServiceDetailEvent : Event<ServiceDetailEvent.PayloadData>
    {

        public ServiceDetailEvent(int RequestId, PayloadData Payload)
            : base(RequestId, Payload)
        { }


        [DataContract]
        public sealed class PayloadData : MessagePayloadBase
        {

            public PayloadData(string VendorName = null, List<ServiceClass> Services = null)
                : base()
            {
                this.VendorName = VendorName;
                this.Services = Services;
            }

            /// <summary>
            /// Freeform string naming the hardware vendor.
            /// <example>ACME ATM Hardware GmbH</example>
            /// </summary>
            [DataMember(Name = "vendorName")]
            public string VendorName { get; init; }

            /// <summary>
            /// Array of one or more services exposed by the publisher.
            /// </summary>
            [DataMember(Name = "services")]
            public List<ServiceClass> Services { get; init; }

        }

    }
}
