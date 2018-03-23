#! /bin/sh

project="<YOUR PROJECT NAME HERE>"

echo "Attempting to build $project for Android"
/Applications/Unity/Unity.app/Contents/MacOS/Unity 
  -batchmode 
  -nographics 
  -logFile $(pwd)/unity.log 
  -projectPath $(pwd) 
  -buildTarget "Android"
  -quit

echo "Attempting to build $project for iOS"
/Applications/Unity/Unity.app/Contents/MacOS/Unity 
  -batchmode 
  -nographics 
  -silent-crashes 
  -logFile $(pwd)/unity.log 
  -projectPath $(pwd) 
  -buildTarget "iOS"
  -quit

echo 'Logs from build'
cat $(pwd)/unity.log
