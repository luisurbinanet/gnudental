# Introduction #

GNUdental, obviously, needs a setup package. We'd like our users to be able to easily install GNUdental on their computers.

# Packages #
## Database ##
The GNUdental data generally lives in a mySQL database. We'll provide a package named `gnudental-database` that creates the database.
Ideally, database upgrades also are handled by this package.

## Server ##
GNUdental comes with a server that uses remoting to talk to the clients and interacts directly with the database. We'll provide a `gnudental-server` package to install the server

## Client ##
The GNUdental client lives in the `gnudental-client` package

## Common ##
Some code is common (OpenDentalBusiness.dll, for example). That should go in `gnudental-common`

# Distribution #
We, for now, only plan to ship Debian and Ubuntu packages. Once we get that stable, we may look into other distributiosn

# Windows #
Using WiX to create a native Windows .MSI package may be worth looking into, but is currently not on the radar.