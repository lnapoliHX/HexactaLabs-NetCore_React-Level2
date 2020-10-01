import api from "../../../common/api";
import { goBack } from "connected-react-router";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setLoading, ActionTypes } from "../list";
import { toast } from "react-toastify";

/* Actions */
function success(product) {
  return {
    type: ActionTypes.CREATE,
    product
  };
}

export function create(product) {
  return function(dispatch) {
    dispatch(setLoading(true));
    product.costPrice = parseFloat(product.costPrice);
    product.salePrice = parseFloat(product.salePrice);
    product.stock = parseInt(product.stock);
    console.log('create', product);
    return api
      .post(`/product/`, product)
      .then(response => {
        if (response.data.success) {
          toast.success("El producto se creó con éxito");
          dispatch(success(response.data.product));
          dispatch(setLoading(false));
          return dispatch(goBack());  
        } else {
          console.log('fail***', response);
          toast.error("El producto ya existe.");
          return dispatch(setLoading(false));  
        }
      })
      .catch(error => {
        console.log('error***', error);
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}

