# Image Conversion Service

The goal of this project is to implement a Web API that converts
common web image file formats (e.g., PNG) into JPEG.

## Installation / General Notes

- Made with Visual Studio Code 2022 on Windows, using sqlite for database.
- docker-compose up -- build to start the main service.
- dotnet run --project tools/Imagination -- to send the test out files.
- Or just use postman with a default POST request at "http://localhost:5000/convert" 
  and send a image file with extensions jpg or png.
- Those Images extensions previously mentioned are rules in Configuration/FileRules.json, 
  I arbitrarily picked and choosed them. Same with Configuration/FileRules.json
