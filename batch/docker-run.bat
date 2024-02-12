:: Date: 2/12/2024
::
:: Created By: Ron Jenkins
::
:: Purpose: Runs the docker image created in local environment. 

docker run -p 6172:443 -p 6170:80  --name ffmpegapi ffmpegapi 
