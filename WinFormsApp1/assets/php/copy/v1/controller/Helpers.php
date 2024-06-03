<?php

class Helpers {

    public static function debug_to_console($data) {
        $output = $data;
        if (is_array($output)) {
            $output = implode(',', $output);
        }
        echo "<script>console.log('Debug Objects: " . $output . "' );</script>";
    }
}

function exception_error_handler($errno, $errstr, $errfile, $errline ) {
    throw new ErrorException($errstr, $errno, 0, $errfile, $errline);
}
set_error_handler("exception_error_handler");