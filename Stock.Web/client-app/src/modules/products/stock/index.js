import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";
import { goBack } from "connected-react-router";
import { pickBy } from "lodash";

/* Actions */
function success(product) {
  return {
    type: ActionTypes.UPDATE,
    product
  };
}

export function stock(product) {
  return function(dispatch) {
    dispatch(setLoading(true));
    return api
      .post("/product/stock", pickBy(product))
      .then(response => {
        toast.success("El stock se actualizó con éxito");
        dispatch(success(response.data));
        dispatch(setLoading(false));
        return dispatch(goBack());
      })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}
