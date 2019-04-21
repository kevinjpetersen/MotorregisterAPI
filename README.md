# Motorregister API
Automatically unpack, generate and add Danish Motorregister Data to a Mongo Database from an XML File.

## Introduction
This repository contains a .NET Core 2.2 (C#) that takes an Danish Motorregister XML File (From SKAT) as Input, and will unpack, generate and add each item to a Mongo Database (However this should be easy to change to another DB if so desired).

## Requirements
* Linux or Windows Server
* Mongo DB Database
* Knowledge of .NET/C# for building the project and making changes accordingly

## How to Generate and Host Data
1. Make changes to the Database settings in the Project, then build it for your desired OS (Linux, Windows or Mac)
2. Host a Linux Server (or Windows server, however I recommend Linux) with a minimum of 100 GB of storage and a decent CPU. This server will be used to unpack and generate the Motorregister Data based on the XML File.
3. Host a Mongo DB which is accessible either on the same server or remotely. This should have a minimum storage of around 50-100 GB aswell.
4. Install .NET Core 2.2 Runtime on the Server
5. Put on the published/build of the .NET Project on the server from step 1.
6. Download the latest ``.zip`` file from SKAT's DMR FTP Server ``ftp://5.44.137.84/ESStatistikListeModtag/`` onto the Linux Server, this will take about 3-4 hours depending on the server's internet speed, however overall this speed is limited by SKAT's servers.
7. Extract the ``.zip`` file to the same folder as the program from step 4.
8. Run the Program / .NET Project on the Server
9. You will be prompted to enter the name of the XML File. Enter the name of the downloaded XML File and press enter.
10. You will be prompted to enter an amount of how many rows/items that should be proccessed, before these items are saved to the database. You don't wanna put this number too low as that will make the whole process take longer. A good number is to set it to around 1000-2000.
11. Wait for the whole thing to finish, this will take about 3-4 hours depending on the server specs like CPU, Disk Speed etc.
12. Whenever the program says "Done", the generation is completed.

## License
This project is licensed under the MIT License, therefore a copyright notice is required to be given.

## Contact
If you want to contact me about this project, you can do so by sending me an e-mail at kevingeeken@gmail.com
