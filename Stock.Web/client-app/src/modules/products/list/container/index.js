import React from "react";
import { connect } from "react-redux";
import { push } from "connected-react-router";
import PropTypes from "prop-types";
import { getProducts, getAllData, fetchByFilters } from "../index";
import { getProductTypes } from "../../../productTypes/list/index"
import { getProviders } from "../../../providers/list/index"
import Presentation from "../presentation";

const initialState = {
  name: "",
  condition: "AND"
};

class ProductsPage extends React.Component {
  constructor(props) {
    super(props);
    this.state = { ...initialState };
  }

  onFilterChange = event => {
    this.setState({ [event.target.name]: event.target.value });
  };

  onFilterSubmit = () => {
    console.log('filterSubmit', this.state);
    this.props.fetchByFilters(this.state);
  };

  onFilterReset = () => {
    this.setState({ ...initialState });
    this.props.getAllData();
  };

  render() {
    const { products, productTypes, providers, loading, ...rest } = this.props;
    console.log('products/list/container');
    console.log('products', products);
    console.log('productTypes', productTypes);
    console.log('providers', providers);

    return (
      <Presentation
        data={products}
        dataLoading={loading}
        defaultPageSize={5}
        filters={this.state}
        onFilterChange={this.onFilterChange}
        onFilterSubmit={this.onFilterSubmit}
        clearFilter={this.onFilterReset}
        {...rest}
      />
    );
  }
}

ProductsPage.propTypes = {
  products: PropTypes.array.isRequired,
  loading: PropTypes.bool.isRequired,
  getAllData: PropTypes.func.isRequired,
  push: PropTypes.func.isRequired,
  fetchByFilters: PropTypes.func.isRequired
};

const mapStateToProps = state => {
  return {
    productTypes: getProductTypes(state),
    providers: getProviders(state),
    products: getProducts(state).map(product => ({
      ...product,
      productTypeDesc: getProductTypes(state).length > 0 ? 
                          getProductTypes(state).find(pt => pt.id === product.productTypeId).description : '',
      providerName: getProviders(state).length > 0 ? 
                          getProviders(state).find(prov => prov.id === product.providerId).name : '',                   
  })),
  }
};

const mapDispatchToProps = {
  getAllData,
  push,
  fetchByFilters
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ProductsPage);
