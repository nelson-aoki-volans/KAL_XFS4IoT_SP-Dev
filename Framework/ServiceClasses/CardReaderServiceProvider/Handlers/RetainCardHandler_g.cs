/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2021
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT CardReader interface.
 * RetainCardHandler_g.cs uses automatically generated parts.
\***********************************************************************************************/


using System;
using System.Threading.Tasks;
using System.Threading;
using XFS4IoT;
using XFS4IoTServer;
using XFS4IoT.CardReader.Commands;
using XFS4IoT.CardReader.Completions;
using IServiceProvider = XFS4IoTServer.IServiceProvider;

namespace XFS4IoTFramework.CardReader
{
    [CommandHandler(XFSConstants.ServiceClass.CardReader, typeof(RetainCardCommand))]
    public partial class RetainCardHandler : ICommandHandler
    {
        public RetainCardHandler(ICommandDispatcher Dispatcher, ILogger logger)
        {
            Dispatcher.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Dispatcher)}");
            Provider = Dispatcher.IsA<IServiceProvider>();

            Provider.Device.IsNotNull($"Invalid parameter received in the {nameof(RetainCardHandler)} constructor. {nameof(Provider.Device)}")
                           .IsA<ICardReaderDevice>();

            CardReader = Provider.IsA<ICardReaderServiceClass>();

            this.Logger = logger.IsNotNull($"Invalid parameter in the {nameof(RetainCardHandler)} constructor. {nameof(logger)}");
        }

        public async Task Handle(IConnection Connection, object command, CancellationToken cancel)
        {
            var retainCardCmd = command.IsA<RetainCardCommand>($"Invalid parameter in the RetainCard Handle method. {nameof(RetainCardCommand)}");
            
            IRetainCardEvents events = new RetainCardEvents(Connection, retainCardCmd.Headers.RequestId);

            var result = await HandleRetainCard(events, retainCardCmd, cancel);
            await Connection.SendMessageAsync(new RetainCardCompletion(retainCardCmd.Headers.RequestId, result));
        }

        public async Task HandleError(IConnection connection, object command, Exception commandException)
        {
            var retainCardcommand = command.IsA<RetainCardCommand>();

            RetainCardCompletion.PayloadData.CompletionCodeEnum errorCode = commandException switch
            {
                InvalidDataException => RetainCardCompletion.PayloadData.CompletionCodeEnum.InvalidData,
                NotImplementedException => RetainCardCompletion.PayloadData.CompletionCodeEnum.UnsupportedCommand,
                _ => RetainCardCompletion.PayloadData.CompletionCodeEnum.InternalError
            };

            var response = new RetainCardCompletion(retainCardcommand.Headers.RequestId, new RetainCardCompletion.PayloadData(errorCode, commandException.Message));

            await connection.SendMessageAsync(response);
        }

        private ICardReaderDevice Device { get => Provider.Device.IsA<ICardReaderDevice>(); }
        private IServiceProvider Provider { get; }
        private ICardReaderServiceClass CardReader { get; }
        private ILogger Logger { get; }
    }

}