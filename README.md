# Folder Synchroniser

## Overview

The **Folder Synchroniser** is a C# application designed to manage the synchronisation of files between two directories. Its primary purpose is to ensure that the contents of a source folder are mirrored in a replica folder. The application supports periodic synchronisation based on a specified interval, logs its activities to a file, and validates user inputs to ensure smooth operation. This utility is ideal for backup solutions, version control, or any task requiring consistent folder replication.

## File Descriptions

### 1. VerifyInput.cs

This static class is responsible for validating and parsing the command-line arguments passed to the application. It ensures:

- The correct number of arguments (4) is provided.

- The synchronisation interval is a valid integer.

- The arguments are packaged into an instance of the `AppConfig` class for further processing.

**Key Function:**

- `ParseArguments`: Parses and validates input arguments, returning an `AppConfig` object or throwing an exception for invalid inputs.

### 2. Logs.cs

This static class handles logging functionalities for the application. Logs are written to a specified file, including timestamps for each entry.

**Key Function:**

`FileWrite`: Writes formatted log messages to a specified log file, including the timestamp in the format specified in the `Config` class.

### 3. FileVerificator.cs

This static class provides functionality for comparing the contents of two files to determine if they are identical. The comparison is performed using MD5 hashing.

**Key Function:**

`AreEqual`: Computes MD5 hashes for two files and returns `true` if they match, indicating identical content.

### 4. Config.cs

This static configuration class defines constants and settings used throughout the application, such as valid file extensions, time formats, and default behaviours for folder and file creation.

**Key Features:**

`ValidExtensions`: Defines allowed file extensions for logs.

`TimeFormat`: Specifies the date-time format for log entries.

`createReplicaFolder` and `createLogFile`: Toggle behaviours for auto-creating missing replica folders and log files.

### 5. AppConfig.cs

This class encapsulates the configuration settings for folder synchronisation, including paths, synchronisation intervals, and log file paths. It validates these settings to ensure they are functional and meet requirements.

**Key Functions:**

Constructor: Initializes the configuration settings and validates them.

`Validate`: Ensures directories and log files exist or creates them if allowed by the configuration.

### 6. Program.cs

The entry point of the application. It initializes the configuration, creates a `FolderSynchroniser` instance, and starts the synchronisation process.

**Key Flow:**

Parses command-line arguments using `VerifyInput`.

Initializes and starts the folder synchronisation process.

Handles exceptions gracefully and exits with an appropriate error message if needed.

## Design Decisions

### Command-Line Interface

The application uses a command-line interface for simplicity and scriptability. This design allows for easy integration into batch processes and scheduled tasks.

### MD5 Hashing for File Comparison

MD5 hashing was chosen for file comparison because it provides a balance between accuracy and performance for determining if files are identical. While MD5 is not cryptographically secure, it is sufficient for the purpose of file synchronisation.

### Log File Management

The application ensures logging functionality by creating a log file if it does not exist, provided this behaviour is enabled in the configuration. This decision was made to guarantee that all activities are tracked, even when initial setup is incomplete.

### Auto-Creation of Folders and Files

The application includes optional features to auto-create missing directories or log files. This reduces user burden and ensures smoother initial setup, especially in automated environments.

### Usage Instructions

### Command-Line Arguments

The application expects four arguments in the following order:

1. **SourcePath**: Path to the source directory.

2. **ReplicaPath**: Path to the replica directory.

3. **SyncIntervalSeconds**: Synchronisation interval in seconds.

4. **LogFilePath**: Path to the log file.

**Example**:

`Folder_Synchroniser.exe C:\Source C:\Replica 60 C:\Logs\sync.log`

### Execution Steps

1. Ensure that the source directory exists and contains the files to be synchronised.

2. Provide valid paths for the replica directory and log file.

3. Specify a synchronisation interval (e.g., `60` for 60 seconds).

4. Run the application with the required arguments.

### Output

- Synchronised contents in the replica directory.

- Log entries detailing synchronisation activities and errors.

### Future Enhancements

-**Real-Time Synchronisation**: Implement a file system watcher to synchronise changes immediately instead of relying on intervals.

-**Enhanced Logging**: Include more detailed log entries, such as file sizes and operation durations.

-**Error Recovery**: Add retry mechanisms for transient errors during synchronisation.

-**GUI Support**: Develop a graphical interface for users less comfortable with command-line operations.

### Conclusion

The Folder Synchroniser is a robust utility for managing directory synchronisation tasks. Its modular design, clear validation logic, and extensibility make it an effective tool for a variety of scenarios.
