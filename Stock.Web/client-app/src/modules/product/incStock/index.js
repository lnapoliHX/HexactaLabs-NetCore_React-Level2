import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";
import { goBack } from "connected-react-router";

/* Actions */
function success(product) {
  return {
    type: ActionTypes.STOCK,
    product,
  };
}

export function increaseStock(product) {
  return function (dispatch) {
    dispatch(setLoading(true));
    return api
      .put(
        `/product/IncreaseStock?id=${product.id}&value=${product.stock}`,
        product
      )
      .then(() => {
        toast.success("Se Incremento el stock con exito");
        dispatch(success(product));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch((error) => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
