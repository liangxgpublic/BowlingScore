# BowlingScore
Rest API project for calculate Bowling Scores.
The solution includes main project "BowlingScore" and unit test project "BowlingScore-Test";
Projects are developed by using Visual Studio 2019 community version;
Web API can be published by Visual studio. For example, published to folder "bin\Release\net5.0\publish".
For End point Test, in tools such as postman, send the Post request which in JSON format, like : 
{
    "pinsDowned" : [10,10,10,10,10,10,10,10,10,10,10,10]
}.
Request will be sent to host : port like http://localhost:5000/Scores  
Response will also be in JSON format.
