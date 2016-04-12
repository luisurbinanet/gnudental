# Introduction #

This page will help you get started with GNUdental<sup>TM</sup> on your Linux computer.

# Development Environment #

First you need to set your development environment properly. Instructions below use an Ubuntu-6.10 installation as an example. Your GNU/Linux distribution may use different names, package management and conventions; use the following as suggestions.

## Development tools and libraries ##
If you are running ubuntu, you would typically do:

`sudo apt-get install
subversion
build-essential
automake1.9
autoconf
libexif-dev
libexpat1-dev
libfontconfig1-dev
libfreetype6-dev
libjpeg62-dev
libpng12-dev
libtiff4-dev
libtiffxx0c2
libungif4-dev
libxft-dev
libxrender-dev
x11proto-render-dev
autotools-dev
bison
dpatch
libtool
libxml2-dev
libxml2-dev
libxslt1-dev
m4
mono
mono-gmcs
mono-gac
mono-utils`

**Notes:**
  1. Mono is currently in the universe repository. Make sure you enable this repository.
  1. You currently need to have the latest version of Mono installed on your computer. To build Mono from SVN, you first need to have a Mono runtime and C# compiler installed on your computer.

## Mono ##
To successfully build GNUdental<sup>TM</sup> you need the SVN version of libgdiplus, mono and mcs.
Follow these instructions for [compiling mono](http://www.mono-project.com/Compiling_Mono).

**Check out and build** libgdiplus, mono and mcs modules.
Run `make` but **do not** run `make install` yet.

If you encounter any problem, that's most likely because of a missing dependency.

**_Do not proceed if you cannot build Mono from source!_**

**Remove the pre-build version of Mono.**

On Ubuntu:
```
sudo apt-get remove mono mono-gmcs mono-gac mono-utils
```

**Install libgdiplus and mono**
```
cd ../libgdiplus
sudo make install
cd ../mono
sudo make install
```

**Which resgen?**

Run
```
cat `which resgen`
```
and see if it invokes `resgen.exe` that is under `/monodir/1.0` or
`/monodir/2.0`. It should be `/monodir/2.0`.

You will find two resgen scripts under `/usr/local/bin` (assuming you installed mono under `/usr/local`): `resgen` pointing to `/monodir/1.0` and `resgen2` pointing to `/monodir/2.0`. Rename `resgen` to `resgen1` and symlink `resgen2` to `resgen`.

```
sudo mv /usr/local/bin/resgen /usr/local/bin/resgen1
sudo ln -s /usr/local/bin/resgen2 /usr/local/bin/resgen
```

Now run `resgen` and see if it supports the `/useSourcePath`.

If it does then you're ok.

## Nant ##
[NAnt](http://nant.sourceforge.net) is used to build GNUdental<sup>TM</sup>. NAnt probably already ships with your distribution, but you need the latest development version (Nightly Build). It is recommented to download the Latest Nightly Build binary; currently [nant-0.86-nightly-2007-03-08](http://nant.sourceforge.net/nightly/latest/nant-bin.tar.gz)  and follow [these installation instructions](http://nant.sourceforge.net/nightly/latest/help/introduction/installation.html).

**Change NAnt.exe.config:**

Run

```
pkg-config --modversion mono
```

If it returns 1.2.3, you should be all set.

_If it returns 1.2.3.50_ _**then**_ you need to overcome a bug in NAnt that makes it invoke an old version of resgen, by doing the following:

To find out where NAnt.exe.config is, do
```
cat `which nant`
```
and find out where `NAnt.exe` hides. Probably, in `/usr/local/nant/bin` if you followed  the above instructions. In that same directory, open
`NAnt.exe.config` and modify it like described in this bug report:
> http://sourceforge.net/tracker/index.php?func=detail&aid=1688162&group_id=31650&atid=402868

(Basically, replace every 1.2.4 by 1.2.3.50).

You are now ready to go!

# Building GNUdental(tm) #

~~First, you need to [check out gnudental svn repository](http://code.google.com/p/gnudental/source). This will create a directory gnudental.~~

Because of the very rapid pace of development on the Open Dental code base, we are currently recommending that developers check code out from [there](https://70.90.133.65:23793/svn/opendental/opendental4.7) and first work on just building and testing that code.  This is a rapidly evolving code tree at the moment and unless you're anxious to make changes that you don't see being made already, we recommend that new developers adopt the role of bug reporter (use the issue tracker in this project because the Bugzilla at Open Dental is open only to reports from developers with write access to that repository) and make a point of building and running as many of the new revisions at that code tree as possible and reporting problems found by doing so [here](http://code.google.com/p/gnudental/issues/list).

More detailed instructions on building the Open Dental code can be found [here](http://opendental.carlier-online.be/).

~~Then, you'll need some dll files.  We are currently basing our build on v4.6.19 of Open Dental, so download http://www.gnudental.org/ODSource-4-6-19.zip and unzip it. For the sake of the following, the directory where you unzip ODSource and the directory where you checked out the sources of GNUdental<sup>TM</sup> are the same.~~

~~Copy the required dll's from ODSource tree to gnudental source tree. Change to the working directory and do the following:~~

~~{{{
cp -a ODSource-4-6-19/Required\ dlls/ gnudental/
cp -a ODSource-4-6-19/SparksToothChart/RequiredDlls/ gnudental/SparksToothChart/
cp -a ODSource-4-6-19/SparksToothChart/TestForm/RequiredDlls/ gnudental/SparksToothChart/TestForm/
}}}~~

~~Next, you'll need to download `Tao-1.3.0-1 Release` in your preferred format from http://www.taoframework.com/Downloads and then unzip it and copy `Tao.OpenGl.ExtensionLoader.dll` to `gnudental/Required dlls/` and `gnudental/SparksToothChart/RequiredDlls/`.~~

Text lined out above applies to building GNUdental(tm) which we don't recommend spending time on right now (this has been true for the past two to three weeks as of 13 April 2007).

When GNUdental(tm) originally forked from Open Dental, we used the latest code available from that project, but that was about 5 weeks ago now, and a great deal has changed in the Open Dental code in that time.  We will eventually replace all of the code in this project's repo (nearly all changes to which were made by Frederik Carlier who went on to incorporate those changes into Open Dental) with that of Open Dental, but we're waiting until the pace of change on Open Dental slows down a bit.  And then we need to decide just what it is we need to do differently than Open Dental---originally, this fork was about getting Open Dental to run on Linux, but that milestone has been [reached](http://www.gnudental.org) (though ATM the binary is not stable and still has many bugs I believe)) thanks to Frederik Carlier, and it appears that this time (different than all of his other promises of the past 3+ years in regards to Linux support), Dr. Sparks was serious when he promised Linux support in Open Dental.  The sole reason for the change in his level of priority for supporting Linux from ("...later... ...not a priority for us now...") to **now!** was the start of the GNUdental(tm) project, but we fully expect that his enthusiasm for such plans will disappear if the GNUdental(tm) project eases up the pressure on him.  Additionally, it's clear that Dr. Sparks differs in his ethics and philosophy from most of the free software community, and also that Open Dental has some major design flaws (such as requiring root user access to the MySQL server in order to function) that have shown no sign of changing over the past two years.  Therefore, even though it's original raison d'être seems to have been OBE due to the spark that we lit under Dr. Sparks' derrière, GNUdental(tm) has more reason than ever to persevere, and I for one (Kevin) will continue this project forever even if for nobody else but my family, because Dr. Sparks has repeatedly demonstrated himself to be a man of low or no ethical values in my opinion so I would never trust my family's business to his questionable sense of ethics.

Now you are ready to build:

```
export MONO_IOMAP=all
make
```

It'll start the build and invoke NAnt.

If the build breaks, check for previously [reported issues](http://code.google.com/p/gnudental/issues/list)

At the moment, the Open Dental code base is at [revision 154](https://code.google.com/p/gnudental/source/detail?r=154) and the GNUdental(tm) code base is at [revision 128](https://code.google.com/p/gnudental/source/detail?r=128).  If you're trying this for the first time, you can expect that:

  1. both of these revisions should **compile** successfully,
  1. both of these revisions should produce two executable files named 'OpenDental.exe' and 'OpenDentServer.exe' in the build tree relative path 'build/mono-2.0.unix/opendental-4.7-debug/bin/'.

If you've checked out these versions and you're finding that one or more of the above is not true for you, then you've probably done something wrong and you should go back, read these instructions again more carefully, and try again.  Feel free to file a bug if you think it is appropriate to do so, however.

Please also note that both of these revisions are expected to break during runtime with various unhandled exceptions.  See the issue tracker for more details.

# Creating the GNUdental(TM) directories #

It is recommended you create a directory structure like this:
  * /home/user/gnudental/data
  * /home/user/gnudental/data/A
  * /home/user/gnudental/data/...
  * /home/user/gnudental/data/Z
  * /home/user/gnudental/export
  * /home/user/gnudental/letter

# Installing the MySQL(R) database #

Get MySQL(R) from your distribution. GNUdental<sup>TM</sup> uses the same database structure as Open Dental. You can get the database creation script from http://www.open-dent.com/opendental.sql.

You'll need to modify the script a little. Remove this:
```
/*
MySQL Backup
Source Host:           localhost
Source Server Version: 4.1.10-nt
Source Database:       opendental
Date:                  2005/03/29 07:56:53
*/
```
and replace it with
```
create database opendental;
```
(the semi-colon is important).

Then, run
```
mysql -h localhost -u root < opendental.sql
```

or, depending on your MySQL configuration,

```
mysql -h localhost -u root -p < opendental.sql
```

and enter your MySQL root password when prompted.

# Running GNUdental(TM) for the first time #
The first time you run GNUdental<sup>TM</sup>, you'll be asked to upgrade your database. Accept to do so. For the data directories, enter the directories you created above. Remember to include a trailing "/" after the directory name. If not, GNUdental<sup>TM</sup> will complain.