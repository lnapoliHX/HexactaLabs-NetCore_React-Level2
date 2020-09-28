import { replace } from "connected-react-router";
import api from "../../../common/api";
import { setLoading, actionTypes } from "../list";
import { toast } from "react-toastify";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

function success(id) {
  return {
    type: actionTypes.REMOVE,
    id
  };
}

function handleError(dispatch, error) {
  apiErrorToast(error);
  
  return dispatch(setLoading(false));
}

export function remove(id) {
  return function (dispatch) {
    dispatch(setLoading(true));
    return api
      .delete(`/producttype/${id}`)
      .then(response => {
        if (!response.data.success) {
          var error = {response: {data: {Message: response.data.message}}};

          dispatch(handleError(dispatch, error));
          return dispatch(replace("/product-type"));
        }

        dispatch(success(id));
        dispatch(setLoading(false));
        toast.success("El tipo se eliminó con éxito");
        
        return dispatch(replace("/product-type"));
      })
      .catch(error => {
        return handleError(dispatch, error);
      });
  };
}
