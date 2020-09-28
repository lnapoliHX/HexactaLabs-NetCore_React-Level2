import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";
import { goBack } from "connected-react-router";

/* Actions */
function success(actionType,product) {
  return {
    type: actionType,
    product
  };
}

export function increase(product) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/product/increaseStock?id=${product.id}&value=${product.value}`)
      .then(() => {
        toast.success("El stock fue modificado");
        dispatch(success(ActionTypes.INCREASE_STOCK,product));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}

export function decrease(product) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .put(`/product/reduceStock?id=${product.id}&value=${product.value}`)
      .then(() => {
        toast.success("El stock fue disminuido");
        dispatch(success(ActionTypes.DECREASE_STOCK,product));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        toast.error("No se puede reducir la cantidad indicad. Stock disponible "+product.stock);
        return dispatch(setLoading(false));
      });
  };
}

