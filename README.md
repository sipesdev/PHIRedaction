# PHI Redaction App

This application includes a React frontend and ASPDOTNET backend API to handle the uploading, processing, and redaction of files containing PHI (Protected Health Information).

## Requirements

Make sure you have the following software installed and included in your system's `$PATH`.

- [.NET 9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node v22.14.0](https://nodejs.org/en)

## Getting Started

1. First clone the project with your prefferred method.

2. Run the setup script:

- Windows: `./setup.bat`.

- Linux/MacOS: `./setup.sh`.

3. Run the entire stack from this script:

- Windows: `./start.bat`

- Linux/MacOS: `./start.sh`

4. The frontend server is hosted at `localhost:3000`. If you are using an API tester, the backend is hosted at `localhost:5128`

## Troubleshooting

- You may have to run `chmod +x setup.sh` on Linux/MacOS if the script doesn't execute.

- The Windows start script will open two cmd prompts, this is normal. (Thanks Microsoft)

- Do not call the scripts directly like this: `start.sh`. The preceding `./` tells the OS to start the scripts in the current path. As such, do not execute the scripts outside of this repo's directory structure. They are programmed with the assumption that both `backend/` and `frontend/` are in the current working directory.

## Known Issues and Potential Notes

- Although it stores the file correctly in the backend as "sample_1_redacted.txt", when sent to the client the file will be renamed to "sample_redacted.txt". This could be due to the poor frontend code I have written to initiate a download.

- If the client's browser knowingly blocks downloads or popups, there is no other way to initiate the download. In the future, it would be better to also add a "CLICK ME IF DOWNLOAD FAILS!" button.

- Regex would be best replaced with AI, as regex does have limitations with this kind of things. Either that or I could separate out all the redaction strategies as Regex strings, then use string concatenation to merge them before compiling.