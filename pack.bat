call %NuGetSynchroniser%
echo off
call del "%~dp0Packaged\Iql.TestBed.*"
call del "%~dp0Packaged\Iql.Tests.*"
call del "%~dp0Packaged\Entity*"
call del "%~dp0Packaged\Brandless*"
echo on