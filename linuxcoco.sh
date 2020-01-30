#!/bin/bash
export DISPLAY=:0
sudo X -nocursor -s 0 -dpms&
mono CocoProject/bin/Debug/CocoCompiler2.exe FormInput.cs&
