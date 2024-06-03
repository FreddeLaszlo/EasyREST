<?php

$filepath = trim(filter_input(INPUT_SERVER, 'REQUEST_URI'), '/');

$path = str_replace('/phpinfo.php', '', $filepath);

$webroot = "/{$path}";
echo $webroot;

$filename = ".htaccess";

$text = "#CORS options (allow from requests from anywhere
#Header Set Access-Control-Allow-Origin \"*\"


# Fix for Apache AUTHORIZATION http header as it is stripped by default for
# security and should be enabled explicitly when needed
#SetEnvIf Authorization .+ HTTP_AUTHORIZATION=$0

# Do not allow file listing if a requested page could not be found
Options -Indexes

# Turn on the rewriting engine
RewriteEngine on
RewriteCond %{REQUEST_FILENAME} !-d
#RewriteCond %{REQUEST_FILENAME} !-f
# Send all requests to default versioned API
RewriteRule . v1/controller/controller.php [END]";

file_put_contents($filename, $text);

$filename = "v1/.htaccess";

$text = "#CORS options (allow from requests from anywhere
#Header Set Access-Control-Allow-Origin \"*\"


# Fix for Apache AUTHORIZATION http header as it is stripped by default for
# security and should be enabled explicitly when needed
SetEnvIf Authorization .+ HTTP_AUTHORIZATION=$0

# Do not allow file listing if a requested page could not be found
Options -Indexes
 
# Send 403 (forbidden) and 404 (file not found) errors to controller 
ErrorDocument 404 {$webroot}/v1/controller/controller.php 
ErrorDocument 403 {$webroot}/v1/controller/controller.php 

# Turn on the rewriting engine
RewriteEngine on
RewriteRule . controller/controller.php [END]";

file_put_contents($filename, $text);

echo "setup complete - click refresh!";