import { cloneDeep, cloneWith, pickBy } from "lodash";
import { normalize } from "../../../common/helpers/normalizer";
import api from "../../../common/api";
import { apiErrorToast } from "../../../common/api/apiErrorToast";

const initialState = {
  loading: false,
  ids: [],
  byId: {},
};

/* Action types */

const LOADING = "PRODUCTS_LOADING";
const SET = "PRODUCTS_SET";
const CREATE = "PRODUCTS_CREATE";
const UPDATE = "PRODUCTS_UPDATE";
const REMOVE = "PRODUCTS_REMOVE";
const STOCK = "PRODUCTS_STOCK";

export const ActionTypes = {
  LOADING,
  SET,
  CREATE,
  UPDATE,
  REMOVE,
  STOCK,
};

/* Reducer handlers */
function handleLoading(state, { loading }) {
  return {
    ...state,
    loading,
  };
}

function handleSet(state, { products }) {
  return {
    ...state,
    ids: products.map((product) => product.id),
    byId: normalize(products),
  };
}

function handleNewproduct(state, { product }) {
  return {
    ...state,
    ids: state.ids.concat([product.id]),
    byId: {
      ...state.byId,
      [product.id]: cloneDeep(product),
    },
  };
}

function handleUpdateproduct(state, { product }) {
  return {
    ...state,
    byId: { ...state.byId, [product.id]: cloneDeep(product) },
  };
}

function handleRemoveproduct(state, { id }) {
  return {
    ...state,
    ids: state.ids.filter((productId) => productId !== id),
    byId: Object.keys(state.byId).reduce(
      (acc, productId) =>
        productId !== `${id}`
          ? { ...acc, [productId]: state.byId[productId] }
          : acc,
      {}
    ),
  };
}

function handleStock(state, { product }) {
  return {
    ...state,
    byId: {
      ...state.byId,
      [product.id]: cloneWith(product, () => {
        product.stock = state.byId[product.id].stock + product.stock;
      }),
    },
  };
}

const handlers = {
  [LOADING]: handleLoading,
  [SET]: handleSet,
  [CREATE]: handleNewproduct,
  [UPDATE]: handleUpdateproduct,
  [REMOVE]: handleRemoveproduct,
  [STOCK]: handleStock,
};

export default function reducer(state = initialState, action) {
  const handler = handlers[action.type];
  return handler ? handler(state, action) : state;
}

/* Actions */
export function setLoading(status) {
  return {
    type: LOADING,
    loading: status,
  };
}

export function setProducts(products) {
  return {
    type: SET,
    products,
  };
}

export function getAll() {
  return (dispatch) => {
    dispatch(setLoading(true));
    return api
      .get("/product")
      .then((response) => {
        dispatch(setProducts(response.data));
        return dispatch(setLoading(false));
      })
      .catch((error) => {
        apiErrorToast(error);
        return dispatch(setLoading(false));
      });
  };
}

export function getById(id) {
  return getAll({ id });
}

export function fetchByFilters(filters) {
  return function (dispatch) {
    return api
      .post("/product/search", pickBy(filters))
      .then((response) => {
        dispatch(setProducts(response.data));
      })
      .catch((error) => {
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

export function getProductsById(state) {
  return base(state).byId;
}

export function getProductIds(state) {
  return base(state).ids;
}

export function getProductById(state, id) {
  return getProductsById(state)[id] || {};
}

function makeGetProductsMemoized() {
  let cache;
  let value = [];
  return (state) => {
    if (cache === getProductsById(state)) {
      return value;
    }
    cache = getProductsById(state);
    value = Object.values(getProductsById(state));
    return value;
  };
}

export const getProducts = makeGetProductsMemoized();
