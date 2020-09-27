import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { goBack } from "connected-react-router";
import { remove } from "../index";
import ProductRemove from "../presentation";

class ProductRemovePage extends React.Component {
  render() {
    return (
      <ProductRemove
        remove={() => this.props.remove(this.props.match.params.id)}
        goBack={this.props.goBack}
      />
    );
  }
}

ProductRemovePage.propTypes = {
  remove: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
  match: PropTypes.object.isRequired,
};

const mapDispatchToProps = { remove, goBack };

export default connect(null, mapDispatchToProps)(ProductRemovePage);
