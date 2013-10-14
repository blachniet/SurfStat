/// <copyright>
/// Copyright (c) 2013
/// </copyright>
/// <author>Brian Lachniet</author>
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;

namespace SurfStat.Test
{
    [TestClass]
    public class SurfStatTests
    {
        [TestMethod]
        public void DefaultConstructorTest()
        {
            var target = new SurfStatFetcher();
            Assert.AreEqual("http://192.168.100.1", target.BaseAddress);
            Assert.AreEqual("/index.cgi?page=modemStatusData", target.ModemStatusUri.ToString());
            Assert.AreEqual("/index.cgi?page=triaStatusData", target.TRIAStatusUri.ToString());
        }

        [TestMethod]
        public void CustomConstructorTest()
        {
            var target = new SurfStatFetcher("http://modem", "/index.cgi?page=testModemData", "/index.cgi?page=testTriaData");
            Assert.AreEqual("http://modem", target.BaseAddress);
            Assert.AreEqual("/index.cgi?page=testModemData", target.ModemStatusUri.ToString());
            Assert.AreEqual("/index.cgi?page=testTriaData", target.TRIAStatusUri.ToString());
        }

        [TestMethod]
        public void ParseModemStatusTest()
        {
            ModemStatus status;
            var target = new SurfStatFetcher();

            const string statusBody = @"55.55.55.55##55:55:55:55:55:55##UT_1.5.2.2.3##UT_7 P3_V1##Online##7,088,473##1,693,689,905##9,378,757##9,532,929,796##000:01:37:54##2##6.2##32%##012345678901##-44.3##47%##1.5##6%##Active##12.4##82%##Single##0123456789##images/Modem_Status_005_Online.png##/images/Satellite_Status_Purple.png##0##<p style=""color:green"">Connected</p>##<p style=""color:green"">Good</p>##0.00%##0.00%##6.67s##195##10000000##";
            Assert.IsTrue(target.TryParseModemStatus(statusBody, out status));

            Assert.AreEqual("Online", status.Status, true);
            Assert.AreEqual(TimeSpan.Parse("000:01:37:54"), status.OnlineTime);

            Assert.AreEqual(-44.3, status.RxPower);
            Assert.AreEqual(0.47, status.RxPowerPercentage);
            Assert.AreEqual(6.2, status.RxSNR);
            Assert.AreEqual(0.32, status.RxSNRPercentage);
            Assert.AreEqual(1.5, status.CableResistance);
            Assert.AreEqual(0.06, status.CableResistancePercentage);
            Assert.AreEqual(12.4, status.CableAttenuation);
            Assert.AreEqual(0.82, status.CableAttenuationPercentage);
            Assert.AreEqual("Active", status.ODUTelemetryStatus, true);

            Assert.AreEqual(IPAddress.Parse("55.55.55.55"), status.IPAddress);
            Assert.AreEqual(PhysicalAddress.Parse("55-55-55-55-55-55"), status.MACAddress);
            Assert.AreEqual("UT_1.5.2.2.3", status.SoftwareVersion);
            Assert.AreEqual("UT_7 P3_V1", status.HardwareVersion);
            Assert.AreEqual("012345678901", status.SerialNumber);
            Assert.AreEqual("0123456789", status.PartNumber);
            Assert.AreEqual("Single", status.IFLType);

            Assert.AreEqual(7088473, status.TransmittedPackets);
            Assert.AreEqual(1693689905, status.TransmittedBytes);
            Assert.AreEqual(9378757, status.ReceivedPackets);
            Assert.AreEqual(9532929796, status.ReceivedBytes);
            Assert.AreEqual(2, status.LossOfSyncCount);

            Assert.AreEqual(@"<p style=""color:green"">Connected</p>", status.StatusHtml);
            Assert.AreEqual(@"<p style=""color:green"">Good</p>", status.HealthHtml);
            Assert.AreEqual("6.67s", status.LastPageLoadDuration);
        }

        [TestMethod]
        public void ParseInvalidModemStatusTest()
        {
            ModemStatus status;
            var target = new SurfStatFetcher();

            // Wrong number of values
            const string statusBody0 = @"55.55.55.55##55:55:55:55:55:55##UT_1.5.2.2.3";            
            Assert.IsFalse(target.TryParseModemStatus(statusBody0, out status));
            Assert.IsNull(status);
            Assert.IsInstanceOfType(target.LastError, typeof(InvalidDataException));
        }

        /// <summary>
        /// Tests that values that cannot be properly parsed are set to defaults (null, NaN, 0)
        /// </summary>
        [TestMethod]
        public void DefaultValuesModemStatusTest()
        {
            ModemStatus status;
            var target = new SurfStatFetcher();
            const string statusBody = @"ip##mac##UT_1.5.2.2.3##UT_7 P3_V1##Online##transp##transb##recp##recb##onlinetime##lossofsync##rxsnr##rxsnrperc##012345678901##rxpow##rxpowperc##cableres##cableresperc##Active##cableatt##cableattperc##Single##0123456789##images/Modem_Status_005_Online.png##/images/Satellite_Status_Purple.png##0##<p style=""color:green"">Connected</p>##<p style=""color:green"">Good</p>##0.00%##0.00%##6.67s##195##10000000##";
            Assert.IsTrue(target.TryParseModemStatus(statusBody, out status));
            Assert.IsNull(status.IPAddress);
            Assert.IsNull(status.MACAddress);
            Assert.AreEqual(0, status.TransmittedPackets);
            Assert.AreEqual(0, status.TransmittedBytes);
            Assert.AreEqual(0, status.ReceivedPackets);
            Assert.AreEqual(0, status.ReceivedBytes);
            Assert.AreEqual(TimeSpan.Zero, status.OnlineTime);
            Assert.AreEqual(0, status.LossOfSyncCount);
            Assert.IsTrue(double.IsNaN(status.RxSNR));
            Assert.AreEqual(0, status.RxSNRPercentage);
            Assert.IsTrue(double.IsNaN(status.RxPower));
            Assert.AreEqual(0, status.RxPowerPercentage);
            Assert.IsTrue(double.IsNaN(status.CableResistance));
            Assert.AreEqual(0, status.CableResistancePercentage);
            Assert.IsTrue(double.IsNaN(status.CableAttenuation));
            Assert.AreEqual(0, status.CableAttenuationPercentage);
        }

        [TestMethod]
        public void ParseTriaStatusTest()
        {
            TriaStatus status;
            var target = new SurfStatFetcher();
            const string statusBody = @"images/green_check_small_002.png##images/green_check_small_002.png##images/green_check_small_002.png##images/green_check_small_002.png##Reduced power##Right##WIN##-21.4##images/green_check_small_002.png##SINGLE##30##images/green_check_small_002.png##images/green_check_small_002.png##images/green_check_small_002.png##2.0##2652##0123456789##35.3##images/green_check_small_002.png##11##images/green_check_small_002.png##11##2##33##13.05##55%##81%##No##No##/images/Satellite_Status_Purple.png##";

            Assert.IsTrue(target.TryParseTriaStatus(statusBody, out status));
            Assert.AreEqual(-21.4, status.TxIFPower);
            Assert.AreEqual(35.3, status.TxRFPower);
            Assert.AreEqual(30.0, status.Temperature);
            Assert.AreEqual(0.55, status.TxIFPowerPercentage);
            Assert.AreEqual(0.81, status.TxRFPowerPercentage);

            Assert.AreEqual("0123456789", status.TriaSerialNumber);
            Assert.AreEqual("/images/Satellite_Status_Purple.png", status.SatelliteStatusImageUri.ToString());
        }

        [TestMethod]
        public void ParseInvalidTriaStatusTest()
        {
            TriaStatus status;
            var target = new SurfStatFetcher();            

            // Wrong number of values
            const string statusBody0 = @"images/green_check_small_002.png##images/green_check_small_002.png##";
            Assert.IsFalse(target.TryParseTriaStatus(statusBody0, out status));
            Assert.IsNull(status);
            Assert.IsInstanceOfType(target.LastError, typeof(InvalidDataException));
        }

        [TestMethod]
        public void DefaultValuesTriaStatusTest()
        {
            TriaStatus status;
            var target = new SurfStatFetcher();            
            target = new SurfStatFetcher();
            const string statusBody1 = @"images/green_check_small_002.png##images/green_check_small_002.png##images/green_check_small_002.png##images/green_check_small_002.png##Reduced power##Right##WIN##txifpower##images/green_check_small_002.png##SINGLE##temperature##images/green_check_small_002.png##images/green_check_small_002.png##images/green_check_small_002.png##2.0##2652##0123456789##txrfpower##images/green_check_small_002.png##11##images/green_check_small_002.png##11##2##33##13.05##txifpowerperc##txrfpowerperc##No##No##/images/Satellite_Status_Purple.png##";
            Assert.IsTrue(target.TryParseTriaStatus(statusBody1, out status));
            Assert.IsTrue(double.IsNaN(status.TxIFPower));
            Assert.IsTrue(double.IsNaN(status.Temperature));
            Assert.IsTrue(double.IsNaN(status.TxRFPower));
            Assert.AreEqual(0, status.TxIFPowerPercentage);
            Assert.AreEqual(0, status.TxRFPowerPercentage);
        }
    }
}
