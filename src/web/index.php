<?php
require_once './assets/php/connection.php';

session_start();

if(isset($_SESSION["user_login"]))	//check condition user login not direct back to index.php page
{
	header("location: ./dashboard");
}

if(isset($_REQUEST['btn_login']))	//button name is "btn_login"
{
	$username	=   strip_tags($_REQUEST["usermail"]);	//textbox name "usermail"
	$email		=   strip_tags($_REQUEST["usermail"]);	//textbox name "usermail"
	$password	=   strip_tags($_REQUEST["password"]);	//textbox name "password"

	if(empty($username)){
		$errorMsg[]="Please enter username or email";	//check "username/email" textbox not empty
	}
	else if(empty($email)){
		$errorMsg[]="Please enter username or email";	//check "username/email" textbox not empty
	}
	else if(empty($password)){
		$errorMsg[]="Please enter password";	//check "passowrd" textbox not empty
	}
	else
	{
		try
		{
			$select_stmt=$db->prepare("SELECT * FROM users WHERE username=:uname OR email=:uemail"); //sql select query
			$select_stmt->execute(array(':uname'=>$username, ':uemail'=>$email));	//execute query with bind parameter
			$row=$select_stmt->fetch(PDO::FETCH_ASSOC);

			if($select_stmt->rowCount() > 0)	//check condition database record greater zero after continue
			{
				if($username==$row["username"] OR $email==$row["email"]) //check condition user taypable "username or email" are both match from database "username or email" after continue
				{
					if(password_verify($password, $row["password"])) //check condition user taypable "password" are match from database "password" using password_verify() after continue
					{
						$_SESSION["user_login"] = $row["user_id"];	//session name is "user_login"
						$loginMsg = "Successfully Login...";		//user login success message
						header("refresh:1; ./dashboard");			//refresh 2 second after redirect to "./dashboard" page
					}
					else
					{
						$errorMsg[]="Wrong password";
					}
				}
				else
				{
					$errorMsg[]="Wrong username or email";
				}
			}
			else
			{
				$errorMsg[]="Wrong username or email";
			}
		}
		catch(PDOException $e)
		{
			$e->getMessage();
		}
	}
}
?>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Login - Smanager</title>
    <link rel="stylesheet" href="assets/css/main/app.css">
    <link rel="stylesheet" href="assets/css/pages/auth.css">
    <link rel="shortcut icon" href="assets/images/logo/favicon.svg" type="image/x-icon">
    <link rel="shortcut icon" href="assets/images/logo/favicon.png" type="image/png">
</head>

<body>
    <div id="auth">
       
<div class="row h-100">
    <div class="col-lg-5 col-12">
        <div id="auth-left">
            <div class="auth-logo">
                <a href="index.php"><img src="assets/images/logo/logo.svg" alt="Logo"></a>
            </div>
            <h1 class="auth-title">Log in.</h1>
            <p class="auth-subtitle mb-5">Log in with your data that you entered during registration.</p>

            <form method="POST">
                <div class="form-group position-relative has-icon-left mb-4">
                    <input type="text" class="form-control form-control-xl" placeholder="Username" name="usermail">
                    <div class="form-control-icon">
                        <i class="bi bi-person"></i>
                    </div>
                </div>
                <div class="form-group position-relative has-icon-left mb-4">
                    <input type="password" class="form-control form-control-xl" placeholder="Password" name="password">
                    <div class="form-control-icon">
                        <i class="bi bi-shield-lock"></i>
                    </div>
                </div> 
                <div>
                <?php
                    if(isset($errorMsg))
                    {
                        foreach($errorMsg as $error)
                        {
                        ?>
                        <div class="alert alert-danger">
                            <strong><?php echo $error; ?></strong>
                        </div>
                            <?php
                        }
                    }
                    if(isset($loginMsg))
                    {
                    ?>
                        <div class="alert alert-success">
                        <strong><?php echo $loginMsg; ?></strong>
                        </div>
                        <?php
                    }
                ?>
                </div>
                <button class="btn btn-primary btn-block btn-lg shadow-lg mt-5" name="btn_login">Log in</button>
            </form> 
            <div class="text-center mt-5 text-lg fs-4">
                <p class="text-gray-600">Don't have an account? <a href="register.php" class="font-bold">Sign
                        up</a>.</p>
                <p><a class="font-bold" href="forgot-password.php">Forgot password?</a>.</p>
            </div>
        </div>
    </div>
    <div class="col-lg-7 d-none d-lg-block">
        <div id="auth-right">
            
        </div>
    </div>
</body>

</html>
