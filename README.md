# SharePoint Event Receiver Manager (2007 & 2010)

**Project Migration Notice:** This project was originally hosted on CodePlex and was officially migrated to GitHub on December 31, 2017.

## Project Description
The SharePoint Event Receiver Manager tool was born out of a personal need for a more polished and professional way to manage event receivers on SharePoint servers. While other tools existed at the time, they often lacked the ease of use required for a painless development workflow. This tool is written in C# and was developed as a "pet project" to provide a more intuitive interface for SharePoint developers.

## Requirements
* **Must be run directly on the SharePoint server** - The tool uses the SharePoint Server Object Model and requires local server access.
* SharePoint Server 2007 or 2010 installed.
* Administrator permissions on the SharePoint server.

## Features
* **Auto Detection:** Automatically detects local SharePoint instances.
* **Validation:** Validates existing event hooks and ensures selected assemblies contain proper event receiver definitions.
* **Property Management:** View and modify event receiver definition properties, including sequence numbers.
* **Assembly Management:** * Select from assemblies already registered in the GAC.
    * Select an assembly file to be automatically registered in the GAC when adding a new event receiver hook.
* **Smart Filtering:** Automatically filters event hooks to show only those defined in a selected event receiver.

## Release History
* **Version 2 (Stable):** Released Nov 28, 2011 — Supports SharePoint 2007 & 2010.
* **Version 1 (Legacy):** Released Mar 28, 2011 — Supports SharePoint 2007 & 2010.

## Screenshots

### Main Form
![Main Form](Home_MainForm1.png)

### Add Dialog
![Add Dialog](Home_AddForm.png)