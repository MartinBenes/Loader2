<?php
$ini = parse_ini_file('config.ini');
$link = mysqli_connect($ini['db_host'],$ini['db_user'],$ini['db_password']);
$database = mysqli_select_db($link,$ini['db_name']);

$user = $_GET['username'];
$tables = $ini['mybb_usertable'];

$sql = "SELECT * FROM ". $tables ." WHERE username = '". mysqli_real_escape_string($link,$user) ."'" ;
$result = $link->query($sql);

if ($result->num_rows > 0){
	while($row = $result->fetch_assoc())
	{
		$groups = $row['usergroup'] . $row['additionalgroups'];
		echo $groups;
	}
} 
else
{
	echo "nou"; // User doesn't exist
}


//-----------------------------------------//
//          Thaisen's Loader v2.0          //
//										   //
//   Copyright © Tyson Prefontaine-McRae   //
// 										   //
// Removing these lines breaks the license //
//-----------------------------------------//
?>

<head>
<title>Checking groups</title>
</head>