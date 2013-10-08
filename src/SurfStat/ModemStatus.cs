using System;
using System.Net;
using System.Net.NetworkInformation;

namespace SurfStat
{
    /// <summary>
    /// Provides information about the modem/IFL cable status.
    /// </summary>
    public class ModemStatus
    {
        public IPAddress IPAddress { get; internal set; }
        public PhysicalAddress MACAddress { get; internal set; }
        public string SoftwareVersion { get; internal set; }
        public string HardwareVersion { get; internal set; }
        public string Status { get; internal set; }
        public long TransmittedPackets { get; internal set; }
        public long TransmittedBytes { get; internal set; }
        public long ReceivedPackets { get; internal set; }
        public long ReceivedBytes { get; internal set; }
        public TimeSpan OnlineTime { get; internal set; }
        public int LossOfSyncCount { get; internal set; }

        /// <summary>
        /// Gets the RX SNR in dB.
        /// </summary>
        public double RxSNR { get; internal set; }
        public double RxSNRPercentage { get; internal set; }
        public string SerialNumber { get; internal set; }

        /// <summary>
        /// Gets the Rx Power in dBm.
        /// </summary>
        public double RxPower { get; internal set; }
        public double RxPowerPercentage { get; internal set; }

        /// <summary>
        /// Gets the Cable Resistance in Ohms.
        /// </summary>
        public double CableResistance { get; internal set; }
        public double CableResistancePercentage { get; internal set; }
        public string ODUTelemetryStatus { get; internal set; }
        public double CableAttenuation { get; internal set; }
        public double CableAttenuationPercentage { get; internal set; }
        public string IFLType { get; internal set; }
        public string PartNumber { get; internal set; }
        public Uri StatusImageUri { get; internal set; }
        public Uri SatelliteStatusUri { get; internal set; }
        public string Unknown25 { get; internal set; }
        public string StatusHtml { get; internal set; }
        public string HealthHtml { get; internal set; }
        public string Unknown28 { get; internal set; }
        public string Unknown29 { get; internal set; }
        public string LastPageLoadDuration { get; internal set; }
        public string Unknown31 { get; internal set; }
        public string Unknown32 { get; internal set; }
    }
}
