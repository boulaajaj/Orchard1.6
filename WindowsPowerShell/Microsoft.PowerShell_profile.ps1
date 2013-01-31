Push-Location (Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)

Push-Location (Split-Path -Path $MyInvocation.MyCommand.Definition -Parent)

# Load posh-hg module from current directory
# Load posh-git example profile
. 'C:\Projects\posh-git\profile.example.ps1'


. .\environment.ps1


# Set up a simple prompt, adding the git/hg/svn prompt parts inside git/hg/svn repos
function prompt {
	Write-Host($pwd) -nonewline
		
	# Git Prompt
	$Global:GitStatus = Get-GitStatus
	$Global:GitPromptSettings.IndexForegroundColor = [ConsoleColor]::Magenta
	Write-GitStatus $GitStatus
		
	# Mercurial Prompt
	$Global:HgStatus = Get-HgStatus
	Write-HgStatus $HgStatus
	  
	return '>
$ '
}

if(-not (Test-Path Function:\DefaultTabExpansion)) {
    Rename-Item Function:\TabExpansion DefaultTabExpansion
}

# Set up tab expansion and include git expansion
function TabExpansion($line, $lastWord) {
	$lastBlock = [regex]::Split($line, '[|;]')[-1]

	switch -regex ($lastBlock) {
		# Execute git tab completion for all git-related commands
		'git (.*)' { GitTabExpansion $lastBlock }
		# mercurial and tortoisehg tab expansion
		'(hg|hgtk) (.*)' { HgTabExpansion($lastBlock) }
		# Development folder tab expansion
		'dev (.*)' { DevTabExpansion $lastBlock }
		# Fall back on existing tab expansion
		default { DefaultTabExpansion $line $lastWord }
	}
}

Pop-Location

function Set-Location-Dev($folder){
	Push-Location
	
	if($folder -ne ""){
		cd "$dev\$folder"
		return
	}
	
	cd $dev
}

function Set-Location-DevProjects($folder){
	Push-Location
	
	if($folder -ne ""){
		cd "$projectsdev\$folder"
		return
	}
	
	cd $projectsdev
}

function Set-Location-DevWebsites($folder){
	Push-Location
	
	if($folder -ne ""){
		cd "$websitesdev\$folder"
		return
	}
	
	cd $websitesdev
}

set-alias dev Set-Location-Dev
set-alias pd Set-Location-DevProjects
set-alias wd Set-Location-DevWebsites

function Set-Location-Profile{
	Push-Location
	cd ~\Documents\WindowsPowerShell
}
set-alias profile Set-Location-Profile

function Start-VisualStudio{
	param([string]$projFile = "")
	
	if($projFile -eq ""){
		ls *.sln | select -first 1 | %{
			$projFile = $_
		}
	}
	
	if(($projFile -eq "") -and (Test-Path src)){
		ls src\*.sln | select -first 1 | %{
			$projFile = $_
		}
	}
	
	if($projFile -eq ""){
		echo "No project file found"
		return
	}
	
	echo "Starting visual studio with $projFile"
	. $projFile
}
set-alias vs Start-VisualStudio

set-alias hosts Set-Hosts

set-alias sms Open_Sql_Management_Studio

set-alias asptemp Remove_Temp_AspNet_Folders

function Kill-All{
	param([string]$name)
	get-process | ?{$_.ProcessName -eq $name -or $_.Id -eq $name} | %{kill $_.Id}
}

function Open_Current_Location_In_Windows_Explorer(){
    invoke-item .
}

function Open_Sql_Management_Studio{
    invoke-item 'C:\Program Files\Microsoft SQL Server\90\Tools\Binn\VSShell\Common7\IDE\SqlWb.exe'
}

function Remove_Temp_AspNet_Folders{
    Get-ChildItem 'C:\WINDOWS\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files' | Remove-Item -recurse
}

#set-alias we Open_Current_Location_In_Windows_Explorer

function Set-Location-Famine($folder){
	Push-Location
	
	if($folder -ne ""){
		cd "$famine\$folder"
		return
	}
	
	cd $famine
}

function Set-Location-Art($folder){
	Push-Location
	
	if($folder -ne ""){
		cd "$art\$folder"
		return
	}
	
	cd $art
}

function Set-Location-Orchard($folder){
	Push-Location
	
	if($folder -ne ""){
		cd "$orchard\$folder"
		return
	}
	
	cd $orchard
}


function Git-Merge-Branch($branchName){
	
	
	if($branchName -ne ""){		
		git merge --no-ff $branchName
		return
	}
}

function Git-Status(){git status}
function Git-Pull(){git pull}
function Git-Fetch(){git fetch}
function Git-Push(){git push origin master}

set-alias we Open_Current_Location_In_Windows_Explorer
set-alias xp Open_Xp_Iis
set-alias vs Start-VisualStudio
set-alias hosts Set-Hosts
set-alias sms Open_Sql_Management_Studio
set-alias smsprev Open_Sql_Management_Studio_2005
set-alias asptemp Remove_Temp_AspNet_Folders
set-alias dev Set-Location-Dev
set-alias pd Set-Location-DevProjects
set-alias wd Set-Location-DevWebsites
set-alias eii Set-Location-Eii
set-alias trans Set-Location-DevTrans
set-alias cms Set-Location-DevCms
set-alias famine Set-Location-Famine
set-alias art Set-Location-Art
set-alias orchard Set-Location-Orchard

set-alias gmerge Git-Merge-Branch
set-alias gpull Git-Pull
set-alias gfetch Git-Fetch
set-alias gpush Git-Push
set-alias gs Git-Status
