import { pickBy } from "lodash";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";
import { setProviders } from "../../providers/list";
import { setProductTypes } from "../../productTypes/list";

const initialState = {
  loading: false,
  products: [],
//  productTypes: [],
//  providers: []
};

/* Action types */

const LOADING = "PRODUCTS_LOADING";
const SET = "PRODUCTS_SET";
const CREATE = "PRODUCTS_CREATE";
const UPDATE = "PRODUCTS_UPDATE";
const REMOVE = "PRODUCTS_REMOVE";

export const ActionTypes = {
  LOADING,
  SET,
  CREATE,
  UPDATE,
  REMOVE
};

/* Reducer handlers */
function handleLoading(state, { loading }) {
  return {
    ...state,
    loading
  };
}

function handleSet(state, { products }) {
  return {
    ...state,
    products,
//    ids: products.map(products => products.id),
//    byId: normalize(products)
  };
}

function handleNewProduct(state, { product }) {
  return {
    ...state,
    products: state.products.concat(product)
  };
}

function handleUpdateProduct(state, { product }) {
  return {
    ...state,
    products: state.products.map(p => (p.id === product.id ? product : p))
  };
}

function handleRemoveProduct(state, { id }) {
  return {
    ...state,
    products: state.products.filter(p => p.id !== id)
  };
}

const handlers = {
  [LOADING]: handleLoading,
  [SET]: handleSet,
  [CREATE]: handleNewProduct,
  [UPDATE]: handleUpdateProduct,
  [REMOVE]: handleRemoveProduct
};

export default function reducer(state = initialState, action) {
  const handler = handlers[action.type];
  return handler ? handler(state, action) : state;
}

/* Actions */
export function setLoading(status) {
  return {
    type: LOADING,
    loading: status
  };
}

export function setProducts(products) {
  return {
    type: SET,
    products
  };
}

export function getAllData() {
  return dispatch => {
    dispatch(setLoading(true));
    return Promise.all([
      api.get("/product"),
      api.get("/producttype"),
      api.get("/provider")])
      .then(([responseProducts, responseProductTypes, responseProviders]) => {
          dispatch(setProducts(responseProducts.data.products));
          dispatch(setProductTypes(responseProductTypes.data.productTypes));
          dispatch(setProviders(responseProviders.data.providers));
          return dispatch(setLoading(false));
        })
      .catch(error => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}

export function getById(id) {
  return getAllData({ id });
}

export function fetchByFilters(filters) {
  return function(dispatch) {
    return api
      .post("/product/search", pickBy(filters))
      .then(response => {
        dispatch(setProducts(response.data.products));
      })
      .catch(error => {
        apiErrorToast(error);
      });
  };
}

/* Selectors */
function base(state) {
  return state.product.list;
}

export function getLoading(state) {
  return base(state).loading;
}

export function getProducts(state) {
  return base(state).products;
}

export function getProductById(state, id) {
  return getProducts(state).find(p => p.id === id);
}
