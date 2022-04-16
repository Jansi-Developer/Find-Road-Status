# Find-Road-Status
Find Road Status Details by Road ID

Key Feature
------------
This application helps to get the status of major roads in London by making HTTP requests to a web server, using async and await, converting JSON objects into C# objects

To Build and Run Application, follow the below steps
----------------------------------------------------

Using .NET Core CLI
-------------------
1. Move to the respective directory. 
   Ex: cd ..\RoadStatus\RoadStatus
2. Type "dotnet build"
3. Type "dotnet run"
4. Give road id to find status
5. Respective output will be shown

Using PowerShell
----------------
1. Move to the respective directory. 
   Ex: cd ..\RoadStatus\RoadStatus
2. Type “.\RoadStatus.exe [road id]”
3. Respective output will be shown
4. Application exits with code 0, if success
5. Application exits with code 1, if failure

Steps to run any test case
--------------------------
1. Modify GetTestRoadDetails List with your own data.
2. Positive Test Cases: To run the below tests please modify the index value of testRoadDetails[0].ID and Assert.AreEqual(testRoadDetails[0].Name,...) with your own data index
    - GetValidRoadStatus1_Test
    - GetValidRoadStatus2_Test
3. Negative Test Cases: To run the above tests please modify the index value of testRoadDetails[2].ID with your own data index
    - GetInValidRoadStatus_Test   

Note: 
-----
1. App_ID and App_Key can be updated in appsettings.json file
2. If road id not given as a input parameter to exe, you will get notification to enter road id

Assumptions
-----------
1. All other errors will be displayed with message “Invalid Request”
2. Validation handled for user input
3. If user missed to give road id while execution, application will not proceed until submitting the valid road id to check status.
4. Success Exit Code is 0 
5. Failure Exit Code is 1
