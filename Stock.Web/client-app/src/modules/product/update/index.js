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
        toast.success("El producto se editó con éxito");
        dispatch(success(response.data.data));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
