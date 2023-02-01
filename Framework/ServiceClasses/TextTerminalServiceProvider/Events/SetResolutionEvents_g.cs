/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT TextTerminal interface.
 * SetResolutionEvents_g.cs uses automatically generated parts.
\***********************************************************************************************/


using XFS4IoT;
using XFS4IoTServer;
using System.Threading.Tasks;

namespace XFS4IoTFramework.TextTerminal
{
    internal class SetResolutionEvents : TextTerminalEvents, ISetResolutionEvents
    {

        public SetResolutionEvents(IConnection connection, int requestId)
            : base(connection, requestId)
        { }

    }
}
