/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CashManagement interface.
 * ISetTellerInfoEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.CashManagement
{
    public interface ISetTellerInfoEvents
    {

        Task TellerInfoChangedEvent(XFS4IoT.CashManagement.Events.TellerInfoChangedEvent.PayloadData Payload);

    }
}
