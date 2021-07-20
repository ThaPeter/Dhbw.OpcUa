# Dhbw.OpcUa
Prüfungsleistung für die Dhbw

# Einleitung
Heutzutage werden moderne Maschinen meist über eine speicherprogrammierbare 
Steuerung (SPS) gesteuert und programmiert. Oft können diese über unterschiedliche 
Protokolle angebunden werden. Open Platform Communications (OPC) hat sich dabei 
als Protokoll behauptet und wird von vielen Engineering Umgebungen automatisch als 
Software-Schnittstelle zum Datentausch angeboten.
Wird eine moderne SPS verwendet, können über dieses Protokoll sämtliche Sensoren 
und Förderbänder oder sogar Lasersteuerungen angesprochen werden, soweit es die 
vorhandene Maschinenprogrammierung zulässt. Auch Wartung und Fehleranalysen 
können über dieses Protokoll durchgeführt werden.

# Problemstellung
Um eine Maschine bzw. einen OPC-Server auszulesen, ist im Normalfall ein Client 
notwendig, der das Protokoll umsetzt und die Daten für den Anwender visualisiert.
Insbesondere für Software-Entwickler sind solche Tools täglich in der Anwendung, 
jedoch ist es meist ungern gesehen, weitere Software auf laufenden Maschinen in der 
Produktion zu installieren. Daher beschäftigt sich dieses Projekt mit der Erstellung 
eines OPC-Clients mit entsprechender Benutzeroberfläche, um OPC-Daten über das 
Handy auszulesen und zu visualisieren.
