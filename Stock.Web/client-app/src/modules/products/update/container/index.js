import React from "react";
import PropTypes from "prop-types";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import { Container, Row, Col } from "reactstrap";
import { getProductById } from "../../list";
import { update } from "..";
import Form from "../../form/presentation";
import { getProductTypes } from "../../../productTypes/list";
import { getProviders } from "../../../providers/list";

const Update = (
  { 
    update: onSubmit, 
    goBack: onCancel,
    productTypeOptions,
    providerOptions,
    initialValues
  }) => {
  return (
    <Container fluid>
      <Row>
        <h2>Edición</h2>
      </Row>
      <Row>
        <Col>
          <Form
            initialValues={initialValues}
            productTypeOptions={productTypeOptions}
            providerOptions={providerOptions}
            onSubmit={onSubmit} 
            handleCancel={onCancel} 
          />
        </Col>
      </Row>
    </Container>
  );
};

Update.propTypes = {
  initialValues: PropTypes.object.isRequired,
  productTypeOptions: PropTypes.array.isRequired,
  providerOptions: PropTypes.array.isRequired,
  update: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
};

const mapStateToProps = (state, ownProps) => {
  const productTypes = getProductTypes(state);
  const providers = getProviders(state);
  return {
    initialValues: getProductById(state, ownProps.match.params.id),
    productTypeOptions: productTypes.map(pt => ({
      id: pt.id,
      label: pt.description,
      value: pt.id
    })),
    providerOptions: providers.map(provider => ({
      id: provider.id,
      label: provider.name,
      value: provider.id
    })),
  }
};

const mapDispatchToProps = {
  update,
  goBack
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
