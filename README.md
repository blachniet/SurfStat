SurfStat
===================

_Neither this project nor its author is associated in any way with **ViaSat**._

This library provides easy access modem status data provided by **ViaSat**'s **SurfBeam&reg; 2** satellite modems' CGI api. 

Get It
--------------

`SurfStat` is available as a NuGet package: https://www.nuget.org/packages/SurfStat/.

    PM> Install-Package SurfStat

Sample Usage
-----------------

Get the modem status asynchronously.

```csharp
var stat = new SurfStatFetcher();
var modemStatus = await stat.GetModemStatusAsync();
if (modemStatus != null)
{
    Console.WriteLine("The modem's external IP address is {0}", status.IPAddress);
}
```

Get the TRIA status synchronously.

```csharp
var stat = new SurfStatFetcher();
var triaStatus = stat.GetTriaStatusAsync().Result;
if (triaStatus != null)
{
    Console.WriteLine("The TRIA's temperature is {0}C", status.Temperature);
}
```

Modem Status
---------------------------------

The **Modem/IFL Cable Status** page is populated with data obtained from requests to `/index.cgi?page=modemStatusData`. The response body is a `##` delimited list of values.

<table><tr><th>Index</th><th>Description</th><th>Sample</th></tr>
<tr><td>0</td>  <td>IP Address</td>                   <td><code>0.0.0.0</code></td></tr>
<tr><td>1</td>  <td>MAC Address</td>                  <td><code>00:00:00:00:00:00</code></td></tr>
<tr><td>2</td>  <td>Software Version</td>             <td><code>UT_1.5.2.2.3</code></td></tr>
<tr><td>3</td>  <td>Hardware Version</td>             <td><code>UT_7 P3_V1</code></td></tr>
<tr><td>4</td>  <td>Status</td>                       <td><code>Scanning, Online</code></td></tr>
<tr><td>5</td>  <td>Transmitted Packets</td>          <td><code>6,946,245</code></td></tr>
<tr><td>6</td>  <td>Transmitted Bytes</td>            <td><code>1,668,108,099</code></td></tr>
<tr><td>7</td>  <td>Received Packets</td>             <td><code>9,226,449</code></td></tr>
<tr><td>8</td>  <td>Received Bytes</td>               <td><code>9,460,216,018</code></td></tr>
<tr><td>9</td>  <td>Online time</td>                  <td><code>000:00:15:16</code></td></tr>
<tr><td>10</td> <td>Loss of Sync Count</td>          <td><code>1</code></td></tr>
<tr><td>11</td> <td>Rx SNR (dB)</td>                 <td><code>5.1</code></td></tr>
<tr><td>12</td> <td>Rx SNR %</td>                    <td><code>28%</code></td></tr>
<tr><td>13</td> <td>Serial Number</td>               <td><code>000000000000</code></td></tr>
<tr><td>14</td> <td>Rx Power (dBm)</td>              <td><code>-45.2</code></td></tr>
<tr><td>15</td> <td>Rx Power %</td>                  <td><code>45%</code></td></tr>
<tr><td>16</td> <td>Cable Resistance (Ohms)</td>     <td><code>1.5</code></td></tr>
<tr><td>17</td> <td>Cable Resistance %</td>          <td><code>6%</code></td></tr>
<tr><td>18</td> <td>ODU Telemetry status</td>        <td><code>Active</code></td></tr>
<tr><td>19</td> <td>Cable Attenuation</td>           <td><code>9.6</code></td></tr>
<tr><td>20</td> <td>Cable Attenuation %</td>         <td><code>80%</code></td></tr>
<tr><td>21</td> <td>IFL Type</td>                    <td><code>Single</code></td></tr>
<tr><td>22</td> <td>Part Number</td>                 <td><code>0000000000</code></td></tr>
<tr><td>23</td> <td>Status Image Url</td>            <td><code>images/Modem_Status_005_Scanning.png</code></td></tr>
<tr><td>24</td> <td>Satellite Status Url</td>        <td><code>/images/Satellite_Status_Purple.png</code></td></tr>
<tr><td>25</td> <td>??</td>                          <td><code>0</code></td></tr>
<tr><td>26</td> <td>Status HTML</td>                 <td><code>&lt;p style="color:green"&gt;Connected&lt;/p&gt;</code></td></tr>
<tr><td>27</td> <td>Health HTML</td>                 <td><code>&lt;p style="color:green"&gt;Good&lt;/p&gt;</code></td></tr>
<tr><td>28</td> <td>??</td>                          <td><code>0.00%</code></td></tr>
<tr><td>29</td> <td>??</td>                          <td><code>0.00%</code></td></tr>
<tr><td>30</td> <td>Last Page Load Duration</td>     <td><code>28.92s</code></td></tr>
<tr><td>31</td> <td>??</td>                          <td><code>197</code></td></tr>
<tr><td>32</td> <td>??</td>                          <td><code>5000000</code></td></tr>
</table>

TRIA Status Value Breakdown
------------------------------

The **TRIA Status** page is populated with data obtained from requests to `/index.cgi?page=triaStatusData`. The response body is a `##` delimited list of values.

<table><tr><th>Index</th><th>Description</th><th>Sample</th></tr>
<tr><td>0</td>  <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>1</td>  <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>2</td>  <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>3</td>  <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>4</td>  <td>?</td>                        <td><code>Reduced power</code></td></tr>
<tr><td>5</td>  <td>?</td>                        <td><code>Right</code></td></tr>
<tr><td>6</td>  <td>?</td>                        <td><code>WIN</code></td></tr>
<tr><td>7</td>  <td>Tx IF Power (dBm)</td>        <td><code>-23.4</code></td></tr>
<tr><td>8</td>  <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>9</td>  <td>?</td>                        <td><code>SINGLE</code></td></tr>
<tr><td>10</td> <td>Temperature (C)</td>          <td><code>20</code></td></tr>
<tr><td>11</td> <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>12</td> <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>13</td> <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>14</td> <td>?</td>                        <td><code>1.5</code></td></tr>
<tr><td>15</td> <td>?</td>                        <td><code>2648</code></td></tr>
<tr><td>16</td> <td>TRIA Serial Number</td>       <td><code>0000000000</code></td></tr>
<tr><td>17</td> <td>Tx RF Power</td>              <td><code>35.7</code></td></tr>
<tr><td>18</td> <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>19</td> <td>?</td>                        <td><code>11</code></td></tr>
<tr><td>20</td> <td>?</td>                        <td><code>images/green_check_small_002.png</code></td></tr>
<tr><td>21</td> <td>?</td>                        <td><code>11</code></td></tr>
<tr><td>22</td> <td>?</td>                        <td><code>2</code></td></tr>
<tr><td>23</td> <td>?</td>                        <td><code>33</code></td></tr>
<tr><td>24</td> <td>?</td>                        <td><code>13.05</code></td></tr>
<tr><td>25</td> <td>Tx IF Power %</td>            <td><code>47%</code></td></tr>
<tr><td>26</td> <td>Tx RF Power %</td>            <td><code>83%</code></td></tr>
<tr><td>27</td> <td>?</td>                        <td><code>No</code></td></tr>
<tr><td>28</td> <td>?</td>                        <td><code>No</code></td></tr>
<tr><td>29</td> <td>Satellite Status Image</td>   <td><code>/images/Satellite_Status_Purple.png</code></td></tr>
</table>
