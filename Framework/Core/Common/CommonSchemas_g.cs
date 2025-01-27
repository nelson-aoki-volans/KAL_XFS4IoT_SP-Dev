/***********************************************************************************************\
 * (C) KAL ATM Software GmbH, 2023
 * KAL ATM Software GmbH licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
 *
 * This file was created automatically as part of the XFS4IoT Common interface.
 * CommonSchemas_g.cs uses automatically generated parts.
\***********************************************************************************************/

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace XFS4IoT.Common
{

    public enum CompletionCodeEnumEnum
    {
        Success,
        CommandErrorCode,
        Canceled,
        DeviceNotReady,
        HardwareError,
        InternalError,
        InvalidCommand,
        InvalidRequestID,
        TimeOut,
        UnsupportedCommand,
        InvalidData,
        UserError,
        UnsupportedData,
        FraudAttempt,
        SequenceError,
        AuthorizationRequired,
        NoCommandNonce,
        InvalidToken,
        InvalidTokenNonce,
        InvalidTokenHMAC,
        InvalidTokenFormat,
        InvalidTokenKeyNoValue,
        NotEnoughSpace
    }


    public enum PositionStatusEnum
    {
        InPosition,
        NotInPosition,
        Unknown
    }


    public enum ExchangeEnum
    {
        NotSupported,
        Active,
        Inactive
    }


    [DataContract]
    public sealed class StatusPropertiesClass
    {
        public StatusPropertiesClass(DeviceEnum? Device = null, PositionStatusEnum? DevicePosition = null, int? PowerSaveRecoveryTime = null, AntiFraudModuleEnum? AntiFraudModule = null, ExchangeEnum? Exchange = null, EndToEndSecurityEnum? EndToEndSecurity = null)
        {
            this.Device = Device;
            this.DevicePosition = DevicePosition;
            this.PowerSaveRecoveryTime = PowerSaveRecoveryTime;
            this.AntiFraudModule = AntiFraudModule;
            this.Exchange = Exchange;
            this.EndToEndSecurity = EndToEndSecurity;
        }

        public enum DeviceEnum
        {
            Online,
            Offline,
            PowerOff,
            NoDevice,
            HardwareError,
            UserError,
            DeviceBusy,
            FraudAttempt,
            PotentialFraud,
            Starting
        }

        /// <summary>
        /// Specifies the state of the device. Following values are possible:
        /// 
        /// * ```online``` - The device is online. This is returned when the device is present and operational.
        /// * ```offline``` - The device is offline (e.g., the operator has taken the device offline by turning a switch or breaking an interlock).
        /// * ```powerOff``` - The device is powered off or physically not connected.
        /// * ```noDevice``` - The device is not intended to be there, e.g. this type of self service machine does not contain such a device or it is internally not configured.
        /// * ```hardwareError``` - The device is inoperable due to a hardware error.
        /// * ```userError``` - The device is present but a person is preventing proper device operation.
        /// * ```deviceBusy``` - The device is busy and unable to process a command at this time.
        /// * ```fraudAttempt``` - The device is present but is inoperable because it has detected a fraud attempt.
        /// * ```potentialFraud``` - The device has detected a potential fraud attempt and is capable of remaining in service. In this case the application should make the decision as to whether to take the device offline.
        /// * ```starting``` - The device is starting and performing whatever initialization is necessary. This can be
        /// reported after the connection is made but before the device is ready to accept commands. This must only be a
        /// temporary state, the Service must report a different state as soon as possible. If an error causes
        /// initialization to fail then the state should change to *hardwareError*.
        /// </summary>
        [DataMember(Name = "device")]
        public DeviceEnum? Device { get; init; }

        /// <summary>
        /// Position of the device. Following values are possible:
        /// 
        /// * ```inPosition``` - The device is in its normal operating position, or is fixed in place and cannot be moved.
        /// * ```notInPosition``` - The device has been removed from its normal operating position.
        /// * ```unknown``` - Due to a hardware error or other condition, the position of the device cannot be determined.
        /// </summary>
        [DataMember(Name = "devicePosition")]
        public PositionStatusEnum? DevicePosition { get; init; }

        /// <summary>
        /// Specifies the actual number of seconds required by the device to resume its normal operational state from
        /// the current power saving mode. This value is 0 if either the power saving mode has not been activated or
        /// no power save control is supported.
        /// </summary>
        [DataMember(Name = "powerSaveRecoveryTime")]
        public int? PowerSaveRecoveryTime { get; init; }

        public enum AntiFraudModuleEnum
        {
            Ok,
            Inoperable,
            DeviceDetected,
            Unknown
        }

        /// <summary>
        /// Specifies the state of the anti-fraud module if available. Following values are possible:
        /// 
        /// * ```ok``` - Anti-fraud module is in a good state and no foreign device is detected.
        /// * ```inoperable``` - Anti-fraud module is inoperable.
        /// * ```deviceDetected``` - Anti-fraud module detected the presence of a foreign device.
        /// * ```unknown``` - The state of the anti-fraud module cannot be determined.
        /// </summary>
        [DataMember(Name = "antiFraudModule")]
        public AntiFraudModuleEnum? AntiFraudModule { get; init; }

        [DataMember(Name = "exchange")]
        public ExchangeEnum? Exchange { get; init; }

        public enum EndToEndSecurityEnum
        {
            NotSupported,
            NotEnforced,
            NotConfigured,
            Enforced
        }

        /// <summary>
        /// Specifies the status of end to end security support on this device. 
        /// 
        /// Also see [Common.CapabilityProperties.endToEndSecurity](#common.capabilities.completion.properties.common.endtoendsecurity). 
        /// 
        /// * ```notSupported``` - E2E security is not supported by this hardware. Any command can be called without a 
        /// token. 
        /// * ```notEnforced``` - E2E security is supported by this hardware but it is not currently enforced, for 
        /// example because required keys aren't loaded. It's currently possible to perform E2E commands without a 
        /// token. 
        /// * ```notConfigured``` - E2E security is supported but not correctly configured, for example because required
        /// keys aren't loaded. Any attempt to perform any command protected by E2E security will fail.
        /// * ```enforced``` - E2E security is supported and correctly configured. E2E security will be enforced. 
        /// Calling E2E protected commands will only be possible if a valid token is given.
        /// <example>enforced</example>
        /// </summary>
        [DataMember(Name = "endToEndSecurity")]
        public EndToEndSecurityEnum? EndToEndSecurity { get; init; }

    }


    [DataContract]
    public sealed class InterfaceClass
    {
        public InterfaceClass(NameEnum? Name = null, Dictionary<string, CommandsClass> Commands = null, Dictionary<string, EventsClass> Events = null, int? MaximumRequests = null)
        {
            this.Name = Name;
            this.Commands = Commands;
            this.Events = Events;
            this.MaximumRequests = MaximumRequests;
        }

        public enum NameEnum
        {
            Common,
            CardReader,
            CashAcceptor,
            CashDispenser,
            CashManagement,
            PinPad,
            Crypto,
            KeyManagement,
            Keyboard,
            TextTerminal,
            Printer,
            BarcodeReader,
            Camera,
            Lights,
            Auxiliaries,
            VendorMode,
            VendorApplication,
            Storage,
            Biometric
        }

        /// <summary>
        /// Name of supported XFS4IoT interface. Following values are supported:
        /// 
        /// * ```Common``` - Common interface. Every device implements this interface.
        /// * ```CardReader``` - CardReader interface.
        /// * ```CashAcceptor``` - CashAcceptor interface.
        /// * ```CashDispenser``` - CashDispenser interface.
        /// * ```CashManagement``` - CashManagement interface.
        /// * ```PinPad``` - PinPad interface.
        /// * ```Crypto``` - Crypto interface.
        /// * ```KeyManagement``` - KeyManagement interface.
        /// * ```Keyboard``` - Keyboard interface.
        /// * ```TextTerminal``` - TextTerminal interface.
        /// * ```Printer``` - Printer interface.
        /// * ```BarcodeReader``` - BarcodeReader interface.
        /// * ```Lights``` - Lights interface.
        /// * ```Auxiliaries``` - Auxiliaries interface.
        /// * ```VendorMode``` - VendorMode interface.
        /// * ```VendorApplication``` - VendorApplication interface.
        /// * ```Storage``` - Storage interface
        /// * ```Biometric``` - Biometric interface
        /// </summary>
        [DataMember(Name = "name")]
        public NameEnum? Name { get; init; }

        [DataContract]
        public sealed class CommandsClass
        {
            public CommandsClass(List<string> Versions = null)
            {
                this.Versions = Versions;
            }

            /// <summary>
            /// The versions of the command supported by the service. There will be one item for each major version
            /// supported. The minor version number qualifies the exact version of the message the service supports.
            /// <example>["1.3", "2.1", "3.0"]</example>
            /// </summary>
            [DataMember(Name = "versions")]
            [DataTypes(Pattern = @"^[1-9][0-9]*\.([1-9][0-9]*|0)$")]
            public List<string> Versions { get; init; }

        }

        /// <summary>
        /// The commands supported by the service.
        /// </summary>
        [DataMember(Name = "commands")]
        public Dictionary<string, CommandsClass> Commands { get; init; }

        [DataContract]
        public sealed class EventsClass
        {
            public EventsClass(List<string> Versions = null)
            {
                this.Versions = Versions;
            }

            /// <summary>
            /// The versions of the event supported by the service. There will be one item for each major version
            /// supported. The minor version number qualifies the exact version of the message the service supports.
            /// <example>["1.3", "2.1", "3.0"]</example>
            /// </summary>
            [DataMember(Name = "versions")]
            [DataTypes(Pattern = @"^[1-9][0-9]*\.([1-9][0-9]*|0)$")]
            public List<string> Versions { get; init; }

        }

        /// <summary>
        /// The events (both event and unsolicited) supported by the service.
        /// </summary>
        [DataMember(Name = "events")]
        public Dictionary<string, EventsClass> Events { get; init; }

        /// <summary>
        /// Specifies the maximum number of requests which can be queued by the Service. This will be omitted if not reported.
        /// This will be 0 if the maximum number of requests is unlimited.
        /// 
        /// </summary>
        [DataMember(Name = "maximumRequests")]
        [DataTypes(Minimum = 0)]
        public int? MaximumRequests { get; init; }

    }


    [DataContract]
    public sealed class FirmwareClass
    {
        public FirmwareClass(string FirmwareName = null, string FirmwareVersion = null, string HardwareRevision = null)
        {
            this.FirmwareName = FirmwareName;
            this.FirmwareVersion = FirmwareVersion;
            this.HardwareRevision = HardwareRevision;
        }

        /// <summary>
        /// Specifies the firmware name. The property is omitted, if the firmware name is unknown.
        /// <example>Acme Firmware</example>
        /// </summary>
        [DataMember(Name = "firmwareName")]
        public string FirmwareName { get; init; }

        /// <summary>
        /// Specifies the firmware version. The property is omitted, if the firmware version is unknown.
        /// <example>1.0.1.2</example>
        /// </summary>
        [DataMember(Name = "firmwareVersion")]
        public string FirmwareVersion { get; init; }

        /// <summary>
        /// Specifies the hardware revision. The property is omitted, if the hardware revision is unknown.
        /// <example>4.3.0.5</example>
        /// </summary>
        [DataMember(Name = "hardwareRevision")]
        public string HardwareRevision { get; init; }

    }


    [DataContract]
    public sealed class SoftwareClass
    {
        public SoftwareClass(string SoftwareName = null, string SoftwareVersion = null)
        {
            this.SoftwareName = SoftwareName;
            this.SoftwareVersion = SoftwareVersion;
        }

        /// <summary>
        /// Specifies the software name. The property is omitted, if the software name is unknown.
        /// <example>Acme Software Name</example>
        /// </summary>
        [DataMember(Name = "softwareName")]
        public string SoftwareName { get; init; }

        /// <summary>
        /// Specifies the software version. The property is omitted, if the software version is unknown.
        /// <example>1.3.0.2</example>
        /// </summary>
        [DataMember(Name = "softwareVersion")]
        public string SoftwareVersion { get; init; }

    }


    [DataContract]
    public sealed class DeviceInformationClass
    {
        public DeviceInformationClass(string ModelName = null, string SerialNumber = null, string RevisionNumber = null, string ModelDescription = null, List<FirmwareClass> Firmware = null, List<SoftwareClass> Software = null)
        {
            this.ModelName = ModelName;
            this.SerialNumber = SerialNumber;
            this.RevisionNumber = RevisionNumber;
            this.ModelDescription = ModelDescription;
            this.Firmware = Firmware;
            this.Software = Software;
        }

        /// <summary>
        /// Specifies the device model name. The property is omitted, if the device model name is unknown.
        /// <example>AcmeModel42</example>
        /// </summary>
        [DataMember(Name = "modelName")]
        public string ModelName { get; init; }

        /// <summary>
        /// Specifies the unique serial number of the device. The property is omitted, if the serial number is unknown.
        /// <example>1.0.12.05</example>
        /// </summary>
        [DataMember(Name = "serialNumber")]
        public string SerialNumber { get; init; }

        /// <summary>
        /// Specifies the device revision number. The property is omitted, if the device revision number is unknown.
        /// <example>1.2.3</example>
        /// </summary>
        [DataMember(Name = "revisionNumber")]
        public string RevisionNumber { get; init; }

        /// <summary>
        /// Contains a description of the device. The property is omitted, if the model description is unknown.
        /// <example>Acme Dispenser Model 3</example>
        /// </summary>
        [DataMember(Name = "modelDescription")]
        public string ModelDescription { get; init; }

        /// <summary>
        /// Array of firmware structures specifying the names and version numbers of the firmware that is present.
        /// Single or multiple firmware versions can be reported. If the firmware versions are not reported, then this property is omitted.
        /// </summary>
        [DataMember(Name = "firmware")]
        public List<FirmwareClass> Firmware { get; init; }

        /// <summary>
        /// Array of software structures specifying the names and version numbers of the software components that are present.
        /// Single or multiple software versions can be reported. If the software versions are not reported, then this property is omitted.
        /// </summary>
        [DataMember(Name = "software")]
        public List<SoftwareClass> Software { get; init; }

    }


    [DataContract]
    public sealed class EndToEndSecurityClass
    {
        public EndToEndSecurityClass(RequiredEnum? Required = null, bool? HardwareSecurityElement = null, ResponseSecurityEnabledEnum? ResponseSecurityEnabled = null, List<string> Commands = null, int? CommandNonceTimeout = null)
        {
            this.Required = Required;
            this.HardwareSecurityElement = HardwareSecurityElement;
            this.ResponseSecurityEnabled = ResponseSecurityEnabled;
            this.Commands = Commands;
            this.CommandNonceTimeout = CommandNonceTimeout;
        }

        public enum RequiredEnum
        {
            IfConfigured,
            Always
        }

        /// <summary>
        /// Specifies the level of support for end to end security
        /// 
        /// * ```ifConfigured``` - The device is capable of supporting E2E security, but it will not be 
        ///   enforced if not configured, for example because the required keys are not loaded.
        /// * ```always``` - E2E security is supported and enforced at all times. Failure to supply the required 
        ///   security details will lead to errors. If E2E security is not correctly configured, for example because 
        ///   the required keys are not loaded, all secured commands will fail with an error.
        /// 
        /// If end to end security is not supported this value will not be present.
        /// <example>always</example>
        /// </summary>
        [DataMember(Name = "required")]
        public RequiredEnum? Required { get; init; }

        /// <summary>
        /// Specifies if this device has a Hardware Security Element (HSE) which validates the security token. 
        /// If this property is false it means that validation is performed in software.
        /// <example>true</example>
        /// </summary>
        [DataMember(Name = "hardwareSecurityElement")]
        public bool? HardwareSecurityElement { get; init; }

        public enum ResponseSecurityEnabledEnum
        {
            NotSupported,
            IfConfigured,
            Always
        }

        /// <summary>
        /// Specifies if this device will return a security token as part of the response data to commands that 
        /// support end to end security, for example, to validate the result of a dispense operation.
        /// 
        /// * ```notSupported``` -  The device is incapable of returning a response token.
        /// * ```ifConfigured``` - The device is capable of supporting E2E security if correctly configured. If E2E 
        ///   security has not been correctly configured, for example because the required keys are not loaded, 
        ///   commands will complete without a security token.
        /// * ```always``` - A security token will be included with command responses. If E2E security is not correctly 
        ///   configured, for example because the required keys are not loaded, the command will complete with an error.
        /// <example>always</example>
        /// </summary>
        [DataMember(Name = "responseSecurityEnabled")]
        public ResponseSecurityEnabledEnum? ResponseSecurityEnabled { get; init; }

        /// <summary>
        /// Array of commands which require an E2E token to authorize. These commands will fail if called without 
        /// a valid token. 
        /// 
        /// The commands that can be listed here depends on the XFS4IoT standard, but it's possible that the standard
        /// will change over time, so for maximum compatibility an application should check this property before calling
        /// a command. 
        /// 
        /// Note that this only includes commands that _require_ a token. Commands that take a nonce and _return_ a 
        /// token will not be listed here. Those commands can be called without a nonce and will continue to operate 
        /// in a compatible way.
        /// <example>["CashDispenser.Dispense"]</example>
        /// </summary>
        [DataMember(Name = "commands")]
        [DataTypes(Pattern = @"^[A-Za-z][A-Za-z0-9]*\.[A-Za-z][A-Za-z0-9]*$")]
        public List<string> Commands { get; init; }

        /// <summary>
        /// If this device supports end to end security and can return a command nonce with the command 
        /// [Common.GetCommandNonce](#common.getcommandnonce), and the device automatically clears the command 
        /// nonce after a fixed length of time, this property will report the number of seconds between returning
        /// the command nonce and clearing it. 
        /// 
        /// The value is given in seconds but it should not be assumed that the timeout will be accurate to the nearest 
        /// second. The nonce may also become invalid before the timeout, for example because of a power failure. 
        /// 
        /// The device may impose a timeout to reduce the chance of an attacker re-using a nonce value or a token. This 
        /// timeout will be long enough to support normal operations such as dispense and present including creating 
        /// the required token on the host and passing it to the device. For example, a command nonce might time out
        /// after one hour (that is, 3600 seconds).
        /// 
        /// In all other cases, commandNonceTimeout will have a value of zero. Any command nonce will never 
        /// timeout. It may still become invalid, for example because of a power failure or when explicitly cleared 
        /// using the ClearCommandNonce command.
        /// <example>3600</example>
        /// </summary>
        [DataMember(Name = "commandNonceTimeout")]
        [DataTypes(Minimum = 0)]
        public int? CommandNonceTimeout { get; init; }

    }


    [DataContract]
    public sealed class CapabilityPropertiesClass
    {
        public CapabilityPropertiesClass(string ServiceVersion = null, List<DeviceInformationClass> DeviceInformation = null, bool? PowerSaveControl = null, bool? AntiFraudModule = null, EndToEndSecurityClass EndToEndSecurity = null)
        {
            this.ServiceVersion = ServiceVersion;
            this.DeviceInformation = DeviceInformation;
            this.PowerSaveControl = PowerSaveControl;
            this.AntiFraudModule = AntiFraudModule;
            this.EndToEndSecurity = EndToEndSecurity;
        }

        /// <summary>
        /// Specifies the Service Version.
        /// <example>1.3.42</example>
        /// </summary>
        [DataMember(Name = "serviceVersion")]
        public string ServiceVersion { get; init; }

        /// <summary>
        /// Array of deviceInformation structures. If the service uses more than one device there will be on array
        /// element for each device.
        /// </summary>
        [DataMember(Name = "deviceInformation")]
        public List<DeviceInformationClass> DeviceInformation { get; init; }

        /// <summary>
        /// Specifies whether power saving control is available.
        /// </summary>
        [DataMember(Name = "powerSaveControl")]
        public bool? PowerSaveControl { get; init; }

        /// <summary>
        /// Specifies whether the anti-fraud module is available.
        /// </summary>
        [DataMember(Name = "antiFraudModule")]
        public bool? AntiFraudModule { get; init; }

        /// <summary>
        /// If this value is present then end to end security is supported. The sub-properties detail exactly 
        /// how it is supported and what level of support is enabled. Also see
        /// [common.StatusProperties.endToEndSecurity](#common.status.completion.properties.common.endtoendsecurity) for the current 
        /// status of end to end security, such as if it is being enforced, or if configuration is required. 
        /// 
        /// If this value is not present then end to end security is not supported by this service.
        /// </summary>
        [DataMember(Name = "endToEndSecurity")]
        public EndToEndSecurityClass EndToEndSecurity { get; init; }

    }


}
