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
        /// <summary>
        /// 
        /// </summary>
        public IPAddress IPAddress { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public PhysicalAddress MACAddress { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string SoftwareVersion { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string HardwareVersion { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public long TransmittedPackets { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public long TransmittedBytes { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public long ReceivedPackets { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public long ReceivedBytes { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan OnlineTime { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public int LossOfSyncCount { get; internal set; }

        /// <summary>
        /// Gets the RX SNR in dB.
        /// </summary>
        public double RxSNR { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double RxSNRPercentage { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string SerialNumber { get; internal set; }

        /// <summary>
        /// Gets the Rx Power in dBm.
        /// </summary>
        public double RxPower { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double RxPowerPercentage { get; internal set; }

        /// <summary>
        /// Gets the Cable Resistance in Ohms.
        /// </summary>
        public double CableResistance { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double CableResistancePercentage { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string ODUTelemetryStatus { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double CableAttenuation { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public double CableAttenuationPercentage { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string IFLType { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string PartNumber { get; internal set; }

        /// <summary>
        /// The URI of the image that appears to in the Status section of the modem/IFL cable status
        /// page. This is a URI relative to the base address of the modem.
        /// </summary>
        public Uri StatusImageUri { get; internal set; }

        /// <summary>
        /// The URI of the image taht appears in the top-right corner of all the modem pages, with
        /// the different satellites. This is a URI relative to the base address of the modem.
        /// </summary>
        public Uri SatelliteStatusUri { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Unknown25 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusHtml { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string HealthHtml { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Unknown28 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Unknown29 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string LastPageLoadDuration { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Unknown31 { get; internal set; }

        /// <summary>
        /// 
        /// </summary>
        public string Unknown32 { get; internal set; }
    }
}
