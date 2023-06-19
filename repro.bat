dotnet build -c release

::\t\hyperfine\hyperfine.exe --warmup 1 --min-runs 20 "C:\proj\rr\bin\release\net8.0\win-x64\publish\rr.exe non"

::\t\hyperfine\hyperfine.exe --warmup 1 --min-runs 20 "C:\proj\rr\bin\release\net8.0\win-x64\publish\rr.exe nbt"

@echo generated regex (suffering from tiering)
hyperfine.exe --warmup 1 --min-runs 20 "dotnet exec bin\release\net8.0\rr.dll gen"

@echo ref emit regex
hyperfine.exe --warmup 1 --min-runs 20 "dotnet exec bin\release\net8.0\rr.dll reg"