using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;

namespace SurfStat
{
    public class SurfStat
    {
        public SurfStat(string baseAddress = "http://192.168.100.1", string modemStatusUri = "/index.cgi?page=modemStatusData", string triaStatusUri = "/index.cgi?page=triaStatusData")
        {
            BaseAddress = baseAddress;
            ModemStatusUri = new Uri(modemStatusUri, UriKind.Relative);
            TRIAStatusUri = new Uri(triaStatusUri, UriKind.Relative);
        }

        /// <summary>
        /// Gets or sets the base address used to access the modem. The default is http://192.168.100.1.
        /// </summary>
        public string BaseAddress { get; set; }

        /// <summary>
        /// Gets or sets the relative Uri to the modem status CGI request. The default is /index.cgi?page=modemStatusData. 
        /// </summary>
        public Uri ModemStatusUri { get; set; }

        /// <summary>
        /// Gets or sets the relative Uri to the TRIA status CGI request. The default is /index.cgi?page=triaStatusData.
        /// </summary>
        public Uri TRIAStatusUri { get; set; }

        /// <summary>
        /// Gets the latest exception causing a call to fail.
        /// </summary>
        public Exception LastError { get; protected set; }

        /// <summary>
        /// Requests the current modem status asynchronously.
        /// </summary>
        /// <returns>The modem status if successfull, null otherwise. If null, <see cref="LastError"/> will be set.</returns>
        public virtual async Task<ModemStatus> GetModemStatusAsync()
        {
            using (var client = new WebClient())
            {
                client.BaseAddress = BaseAddress;
                var statusBody = await client.DownloadStringTaskAsync(ModemStatusUri);

                ModemStatus result;
                return TryParseModemStatus(statusBody, out result) ? result : null;
            }
        }

        /// <summary>
        /// Requests the current TRIA status asynchronously.
        /// </summary>
        /// <returns>The TRIA status if successfull, null otherwise. If null, <see cref="LastError"/> will be set.</returns>
        public virtual async Task<TriaStatus> GetTriaStatusAsync()
        {
            using (var client = new WebClient())
            {
                client.BaseAddress = BaseAddress;
                var statusBody = await client.DownloadStringTaskAsync(TRIAStatusUri);

                TriaStatus result;
                return TryParseTriaStatus(statusBody, out result) ? result : null;
            }
        }

        /// <summary>
        /// Parses a status from a modem status response.
        /// </summary>
        /// <param name="statusBody">The body of the HTTP response for the modem status.</param>
        /// <param name="status">Outputs the parsed status, or null on failure.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public virtual bool TryParseModemStatus(string statusBody, out ModemStatus status)
        {
            status = null;
            var parts = statusBody.Trim().Split(new[] { "##" }, StringSplitOptions.None);
            if (parts.Length != 34)
            {
                LastError = new InvalidDataException("The status body does not contain the expected number of values.");
                return false;
            }

            try
            {
                status = new ModemStatus();

                IPAddress ip;
                if (IPAddress.TryParse(parts[0], out ip))
                    status.IPAddress = ip;

                PhysicalAddress mac;
                if (TryParsePhysicalAddress(parts[1].Replace(":", "-").ToUpper(), out mac))
                    status.MACAddress = mac;

                status.SoftwareVersion = parts[2];
                status.HardwareVersion = parts[3];
                status.Status = parts[4];

                status.TransmittedPackets = ParseOrZeroLong(parts[5].Replace(",", ""));
                status.TransmittedBytes = ParseOrZeroLong(parts[6].Replace(",", ""));
                status.ReceivedPackets = ParseOrZeroLong(parts[7].Replace(",", ""));
                status.ReceivedBytes = ParseOrZeroLong(parts[8].Replace(",", ""));

                TimeSpan onlineTime;
                if (TimeSpan.TryParse(parts[9], out onlineTime))
                    status.OnlineTime = onlineTime;

                status.LossOfSyncCount = ParseOrZeroInt(parts[10]);
                status.RxSNR = ParseOrNaNDouble(parts[11]);
                status.RxSNRPercentage = ParseOrZeroDouble(parts[12].Replace("%", "")) / 100.0;
                status.SerialNumber = parts[13];
                status.RxPower = ParseOrNaNDouble(parts[14]);
                status.RxPowerPercentage = ParseOrZeroDouble(parts[15].Replace("%", "")) / 100.0;
                status.CableResistance = ParseOrNaNDouble(parts[16]);
                status.CableResistancePercentage = ParseOrZeroDouble(parts[17].Replace("%", "")) / 100.0;
                status.ODUTelemetryStatus = parts[18];
                status.CableAttenuation = ParseOrNaNDouble(parts[19]);
                status.CableAttenuationPercentage = ParseOrZeroDouble(parts[20].Replace("%", "")) / 100.0;
                status.IFLType = parts[21];
                status.PartNumber = parts[22];
                status.StatusImageUri = new Uri(parts[23], UriKind.Relative);
                status.SatelliteStatusUri = new Uri(parts[24], UriKind.Relative);
                status.Unknown25 = parts[25];
                status.StatusHtml = parts[26];
                status.HealthHtml = parts[27];
                status.Unknown28 = parts[28];
                status.Unknown29 = parts[29];
                status.LastPageLoadDuration = parts[30];
                status.Unknown31 = parts[31];
                status.Unknown32 = parts[32];

                return true;
            }
            catch (Exception ex)
            {
                status = null;
                LastError = new InvalidDataException("The status body has an unexpected format.", ex);
                return false;
            }
        }

        /// <summary>
        /// Parses a status from a TRIA status response
        /// </summary>
        /// <param name="statusBody">The body of the HTTP response for the TRIA status.</param>
        /// <param name="status">Outputs the parsed status, or null on failure.</param>
        /// <returns>True if successfull, false otherwise.</returns>
        public virtual bool TryParseTriaStatus(string statusBody, out TriaStatus status)
        {
            status = null;
            var parts = statusBody.Trim().Split(new[] { "##" }, StringSplitOptions.None);
            if (parts.Length != 31)
            {
                LastError = new InvalidDataException("The status body does not contain the expected number of values.");
                return false;
            }

            try
            {
                status = new TriaStatus();
                status.Unknown0 = parts[0];
                status.Unknown1 = parts[1];
                status.Unknown2 = parts[2];
                status.Unknown3 = parts[3];
                status.Unknown4 = parts[4];
                status.Unknown5 = parts[5];
                status.Unknown6 = parts[6];
                status.TxIFPower = ParseOrNaNDouble(parts[7]);
                status.Unknown8 = parts[8];
                status.Unknown9 = parts[9];
                status.Temperature = ParseOrNaNDouble(parts[10]);
                status.Unknown11 = parts[11];
                status.Unknown12 = parts[12];
                status.Unknown13 = parts[13];
                status.Unknown14 = parts[14];
                status.Unknown15 = parts[15];
                status.TriaSerialNumber = parts[16];
                status.TxRFPower = ParseOrNaNDouble(parts[17]);
                status.Unknown18 = parts[18];
                status.Unknown19 = parts[19];
                status.Unknown20 = parts[20];
                status.Unknown21 = parts[21];
                status.Unknown22 = parts[22];
                status.Unknown23 = parts[23];
                status.Unknown24 = parts[24];
                status.TxIFPowerPercentage = ParseOrZeroDouble(parts[25].Replace("%", "")) / 100.0;
                status.TxRFPowerPercentage = ParseOrZeroDouble(parts[26].Replace("%", "")) / 100.0;
                status.Unknown27 = parts[27];
                status.Unknown28 = parts[28];
                status.SatelliteStatusImageUri = new Uri(parts[29], UriKind.Relative);
                return true;
            }
            catch (Exception ex)
            {
                status = null;
                LastError = new InvalidDataException("The status body has an unexpected format.", ex);
                return false;
            }
        }

        #region Parsing Helpers

        private static bool TryParsePhysicalAddress(string macStr, out PhysicalAddress mac)
        {
            try
            {
                mac = PhysicalAddress.Parse(macStr);
                return true;
            }
            catch (Exception)
            {
                mac = null;
                return false;
            }
        }

        private static long ParseOrZeroLong(string longStr)
        {
            long num;
            return long.TryParse(longStr, out num)
                ? num
                : 0;
        }

        private static int ParseOrZeroInt(string intStr)
        {
            int num;
            return int.TryParse(intStr, out num)
                ? num
                : 0;
        }

        private static double ParseOrZeroDouble(string doubleStr)
        {
            double num;
            return double.TryParse(doubleStr, out num)
                ? num
                : 0.0;
        }

        private static double ParseOrNaNDouble(string doubleStr)
        {
            double num;
            return double.TryParse(doubleStr, out num)
                ? num
                : double.NaN;
        }

        #endregion
    }
}
