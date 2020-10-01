import { toast } from "react-toastify";
import { goBack } from "connected-react-router";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";

/* Actions */
function success(product) {
  return {
    type: ActionTypes.UPDATE,
    product
  };
}

export function update(product) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/product/${product.id}`, product)
      .then(response => {
        if (response.data.success) {
          toast.success("El producto se editó con éxito");
          dispatch(success(response.data.product));
          dispatch(setLoading(false));
          return dispatch(goBack()); 
        } else {
          toast.error("El producto ya existe.");
          return dispatch(setLoading(false));  
        }
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
