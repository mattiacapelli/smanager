<?php
    if (isset($_POST['submit'])) {
        require_once './assets/php/connection.php';
        $username = $_POST['username'];
        $email = $_POST['email'];
        $password = $_POST['password'];
        $password2 = $_POST['password2'];
        
        echo $_POST;

        $username = strip_tags($username);
        $email = strip_tags($email);
        $password = strip_tags($password);
        $password2 = strip_tags($password2);
        
        if($password == $password2)
        {
            $sql = 'INSERT INTO users (name, surname, email, class, username, password) VALUES (:name, :surname, :email, :class, :username, :password)';
            $stmt = $db->prepare($sql);
            $stmt->execute(['name' => $name, 'surname' => $surname, 'email' => $email, 'class' => $class, 'username' => $username, 'password' => $pass]);

            if ($stmt->rowCount() > 0) {
                $loginMsg[] = "You have been registered!";
            } else {
                $errorMsg[] = "Error, please try again later.";
            }
        }
        else
        {
            $errorMsg[] = "Passwords do not match";
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
            <h1 class="auth-title">Sign Up</h1>
            <p class="auth-subtitle mb-5">Input your data to register to our website.</p>

            <form method="post">
                <div class="form-group position-relative has-icon-left mb-4">
                    <input type="text" class="form-control form-control-xl" placeholder="Email" name="email">
                    <div class="form-control-icon">
                        <i class="bi bi-envelope"></i>
                    </div>
                </div>
                <div class="form-group position-relative has-icon-left mb-4">
                    <input type="text" class="form-control form-control-xl" placeholder="Username" name="username">
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
                <div class="form-group position-relative has-icon-left mb-4">
                    <input type="password" class="form-control form-control-xl" placeholder="Confirm Password" name="password2">
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
                <button class="btn btn-primary btn-block btn-lg shadow-lg mt-5">Sign Up</button>
            </form>
            <div class="text-center mt-5 text-lg fs-4">
                <p class='text-gray-600'>Already have an account? <a href="index.php" class="font-bold">Log
                        in</a>.</p>
            </div>
        </div>
    </div>
    <div class="col-lg-7 d-none d-lg-block">
        <div id="auth-right">

        </div>
    </div>
</div>

    </div>
</body>

</html>
