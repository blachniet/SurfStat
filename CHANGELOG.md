SurfStat Changelog
==================

1.1.0
------

Updated parsing mechanisms to be more resilient by adding sensible defaults instead of throwing exceptions.

- Updated `ModemStatus` to have sensible defaults when unable to parse
	- **`null`**: `IPAddress`, `MACAddress`
	- **`0`**: `TransmittedPackets`, `TransmitttedBytes`, `ReceivedPackets`, `ReceivedBytes`, `LossOfSyncCount`, `*Percentage`
	- **`TimeSpan.Zero`**: `OnlineTime`
	- **`NaN`**: `RxSNR`, `RxPower`, `CableResistance`, `CableAttenuation`
- Update `TriaStatus` to have sensible defaults when unable to parse
	- **`NaN`**: `TxIFPower`, `Temperature`, `TxRFPower`
	- **`0`**: `TxIFPowerPercentage`, `TxRFPowerPercentage`

1.0.0
------

Initial release providing the following features.

- Retrieve the current modem/IFL cable status.
- Retrieve the current TRIA status.