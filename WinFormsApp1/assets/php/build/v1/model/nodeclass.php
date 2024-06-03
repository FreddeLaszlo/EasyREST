<?php
/**
 * Description of bookshops_2
 *
 * Auto generated for endpoint
 * {endpoint}
 */
class {nodeclass} {

    public function getResult($method, $node) {
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
