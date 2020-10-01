import React, { Component } from "react";
import PropType from "prop-types";
import { push } from "connected-react-router";
import { Route } from "react-router-dom";
import { connect } from "react-redux";
import { getProductById } from "../../list/index";
import Presentation from "../presentation";
import Remove from "../../remove/container";
import { getProductTypeById } from "../../../productTypes/list";
import { getProviderById } from "../../../providers/list";

export class ProductViewPage extends Component {
  render() {
    return (
      <React.Fragment>
        <Presentation product={this.props.product} {...this.props} />
        <Route path="/product/view/:id/remove" component={Remove} />
      </React.Fragment>
    );
  }
}

ProductViewPage.propTypes = {
  product: PropType.object.isRequired
};

const mapStateToProps = (state, ownProps) => {
  const product = getProductById(state, ownProps.match.params.id);
  const productType = getProductTypeById(state, product.productTypeId);
  const provider = getProviderById(state, product.providerId);
  product.productTypeDesc = productType.description;
  product.providerName = provider.name;
  return {
    product: product,
  };
};

const mapDispatchToProps = {
  push
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(ProductViewPage);
