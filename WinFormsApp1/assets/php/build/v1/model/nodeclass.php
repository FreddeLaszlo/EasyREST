<?php
/**
 * Description of {nodeclass}
 *
 * Auto generated for endpoint
 * {endpoint}
 */
class {nodeclass} {

    public function getResult($method, $node, $params) {
        switch($method) {
            {switch}
            default:
                $res = new Response(Response::httpResponseNotImplemented, false, "Method for this endpoint not found.");
                $res->send();
                break;
        }
    }
    
    {GET_FUNC}

    {POST_FUNC}

    {PATCH_FUNC}

    {DELETE_FUNC}
}
