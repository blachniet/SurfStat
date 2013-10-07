using System;

namespace SurfStat
{
    /// <summary>
    /// Provides information about the modem/IFL cable status.
    /// </summary>
    public class TriaStatus
    {
        public string Unknown0 { get; internal set; }
        public string Unknown1 { get; internal set; }
        public string Unknown2 { get; internal set; }
        public string Unknown3 { get; internal set; }
        public string Unknown4 { get; internal set; }
        public string Unknown5 { get; internal set; }
        public string Unknown6 { get; internal set; }

        /// <summary>
        /// Gets the Tx IF Power in dBm.
        /// </summary>
        public double TxIFPower { get; internal set; }
        public string Unknown8 { get; internal set; }
        public string Unknown9 { get; internal set; }

        /// <summary>
        /// Gets the temperature in celsius.
        /// </summary>
        public double Temperature { get; internal set; }
        public string Unknown11 { get; internal set; }
        public string Unknown12 { get; internal set; }
        public string Unknown13 { get; internal set; }
        public string Unknown14 { get; internal set; }
        public string Unknown15 { get; internal set; }
        public string TriaSerialNumber { get; internal set; }
        public double TxRFPower { get; internal set; }
        public string Unknown18 { get; internal set; }
        public string Unknown19 { get; internal set; }
        public string Unknown20 { get; internal set; }
        public string Unknown21 { get; internal set; }
        public string Unknown22 { get; internal set; }
        public string Unknown23 { get; internal set; }
        public string Unknown24 { get; internal set; }
        public double TxIFPowerPercentage { get; internal set; }
        public double TxRFPowerPercentage { get; internal set; }
        public string Unknown27 { get; internal set; }
        public string Unknown28 { get; internal set; }
        public Uri SatelliteStatusImageUri { get; internal set; }
    }
}
