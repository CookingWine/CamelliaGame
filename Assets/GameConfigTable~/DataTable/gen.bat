set WORKSPACE=..\..
set LUBAN_DLL=%WORKSPACE%\Tools~\Luban\Luban.dll
set CONF_ROOT=.
set OutputDataConfigDir=%WORKSPACE%\HtofixAssets\GameDataTable
set OutputDataCodeDir=%WORKSPACE%\Code\DataTableCode
dotnet %LUBAN_DLL% ^
    -t all ^
    -c cs-simple-json ^
    -d json ^
    --conf %CONF_ROOT%\luban.conf ^
    -x outputDataDir=%WORKSPACE%\HtofixAssets\GameDataTable ^
    -x outputCodeDir=%WORKSPACE%\Code\DataTableCode 

pause

#set GEN_CLIENT={Luban.dll的路径}
#set CONF_ROOT={DataTables目录的路径}
#x outputCodeDir={生成的代码的路径}
#x outputDataDir={生成的数据的路径}
#
#
#
#
