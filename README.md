SurfStat
===================

*Neither this project nor its author is associated in any way with **ViaSat***.

This library provides easy access modem status data provided by **ViaSat**'s **SurfBeam&reg; 2** satellite modems' CGI api. 

Sample Usage
-----------------

Get the modem status asynchronously.

    var stat = new SurfStat();
    var modemStatus = await stat.GetModemStatusAsync();
    if (modemStatus != null)
    {
        Console.WriteLine("The modem's external IP address is {0}", status.IPAddress);
    }

Get the TRIA status synchronously

    var stat = new SurfStat();
    var triaStatus = stat.GetTriaStatusAsync().Result;
    if (triaStatus != null)
    {
        Console.WriteLine("The TRIA's temperature is {0}C", status.Temperature);
    }

Modem Status
---------------------------------

The **Modem/IFL Cable Status** page is populated with data obtained from requests to `/index.cgi?page=modemStatusData`. The response body is a `##` delimited list of values.

<table>
<tr><th>Index</th><th>Description</th><th>Sample</th>
<tr><td>0</td>	<td>IP Address</td>                   <td>`0.0.0.0`</td></tr>
<tr><td>1</td>	<td>MAC Address</td>                  <td>`00:00:00:00:00:00`</td></tr>
<tr><td>2</td>	<td>Software Version</td>             <td>`UT_1.5.2.2.3`</td></tr>
<tr><td>3</td>	<td>Hardware Version</td>             <td>`UT_7 P3_V1`</td></tr>
<tr><td>4</td>	<td>Status</td>                       <td>`Scanning`, `Online`</td></tr>
<tr><td>5</td>	<td>Transmitted Packets</td>          <td>`6,946,245`</td></tr>
<tr><td>6</td>	<td>Transmitted Bytes</td>            <td>`1,668,108,099`</td></tr>
<tr><td>7</td>	<td>Received Packets</td>             <td>`9,226,449`</td></tr>
<tr><td>8</td>	<td>Received Bytes</td>               <td>`9,460,216,018`, `9,466,934,881`</td></tr>
<tr><td>9</td>	<td>Online time</td>                  <td>`000:00:15:16`</td></tr>
<tr><td>10</td>	<td>Loss of Sync Count</td>          <td>`0`, `1`</td></tr>
<tr><td>11</td>	<td>Rx SNR (dB)</td>                 <td>`0.0`, `5.1`</td></tr>
<tr><td>12</td>	<td>Rx SNR %</td>                    <td>`10%`, `28%`</td></tr>
<tr><td>13</td>	<td>Serial Number</td>               <td>`000000000000`</td></tr>
<tr><td>14</td>	<td>Rx Power (dBm)</td>              <td>`-45.2`</td></tr>
<tr><td>15</td>	<td>Rx Power %</td>                  <td>`39%`, `45%`</td></tr>
<tr><td>16</td>	<td>Cable Resistance (Ohms)</td>     <td>`2.0`, `1.5`</td></tr>
<tr><td>17</td>	<td>Cable Resistance %</td>          <td>`9%`, `6%`</td></tr>
<tr><td>18</td>	<td>ODU Telemetry status</td>        <td>`Active`</td></tr>
<tr><td>19</td>	<td>Cable Attenuation</td>           <td>`9.6`</td></tr>
<tr><td>20</td>	<td>Cable Attenuation %</td>         <td>`80%`</td></tr>
<tr><td>21</td>	<td>IFL Type</td>                    <td>`Single`</td></tr>
<tr><td>22</td>	<td>Part Number</td>                 <td>`0000000000`</td></tr>
<tr><td>23</td>	<td>Status Image Url</td>            <td>`images/Modem_Status_005_Scanning.png`, `images/Modem_Status_005_Online.png`</td></tr>
<tr><td>24</td>	<td>Satellite Status Url</td>        <td>`/images/Satellite_Status_Purple.png`</td></tr>
<tr><td>25</td>	<td>??</td>                          <td>`0`</td></tr>
<tr><td>26</td>	<td>Status HTML</td>                 <td>`&lt;p style="color:green"&gt;Connected&lt;/p&gt;`</td></tr>
<tr><td>27</td>	<td>Health HTML</td>                 <td>`&lt;p style="color:green"&gt;Good&lt;/p&gt;`</td></tr>
<tr><td>28</td>	<td>??</td>                          <td>`0.00%`</td></tr>
<tr><td>29</td>	<td>??</td>                          <td>`0.00%`</td></tr>
<tr><td>30</td>	<td>Last Page Load Duration</td>     <td>`28.92s`</td></tr>
<tr><td>31</td>	<td>??</td>                          <td>`197`</td></tr>
<tr><td>32</td>	<td>??</td>                          <td>`5000000`</td></tr>
</table>

TRIA Status Value Breakdown
------------------------------

The **TRIA Status** page is populated with data obtained from requests to `/index.cgi?page=triaStatusData`. The response body is a `##` delimited list of values.

<table>
<tr><td>0</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>1</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>2</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>3</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>4</td>	<td>?</td>                        <td>`Reduced power`</td></tr>
<tr><td>5</td>	<td>?</td>                        <td>`Right`</td></tr>
<tr><td>6</td>	<td>?</td>                        <td>`WIN`</td></tr>
<tr><td>7</td>	<td>Tx IF Power (dBm)</td>        <td>`-23.4`</td></tr>
<tr><td>8</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>9</td>	<td>?</td>                        <td>`SINGLE`</td></tr>
<tr><td>10</td>	<td>Temperature (C)</td>          <td>`20`</td></tr>
<tr><td>11</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>12</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>13</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>14</td>	<td>?</td>                        <td>`1.5`</td></tr>
<tr><td>15</td>	<td>?</td>                        <td>`2648`</td></tr>
<tr><td>16</td>	<td>TRIA Serial Number</td>       <td>`0000000000`</td></tr>
<tr><td>17</td>	<td>Tx RF Power</td>              <td>`35.7`</td></tr>
<tr><td>18</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>19</td>	<td>?</td>                        <td>`11`</td></tr>
<tr><td>20</td>	<td>?</td>                        <td>`images/green_check_small_002.png`</td></tr>
<tr><td>21</td>	<td>?</td>                        <td>`11`</td></tr>
<tr><td>22</td>	<td>?</td>                        <td>`2`</td></tr>
<tr><td>23</td>	<td>?</td>                        <td>`33`</td></tr>
<tr><td>24</td>	<td>?</td>                        <td>`13.05`</td></tr>
<tr><td>25</td>	<td>Tx IF Power %</td>            <td>`47%`</td></tr>
<tr><td>26</td>	<td>Tx RF Power %</td>            <td>`83%`</td></tr>
<tr><td>27</td>	<td>?</td>                        <td>`No`</td></tr>
<tr><td>28</td>	<td>?</td>                        <td>`No`</td></tr>
<tr><td>29</td>	<td>Satellite Status Image</td>   <td>`/images/Satellite_Status_Purple.png`</td></tr>
</table>
