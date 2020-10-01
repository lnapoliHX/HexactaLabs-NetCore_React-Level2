import React from "react";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import { create } from "../index";
import Form from "../../form/presentation";
import PropTypes from "prop-types";
import { Container, Row, Col, Alert } from "reactstrap";
import { Link } from "react-router-dom";
import { getProductTypes } from "../../../productTypes/list";
import { getProviders } from "../../../providers/list";

const Create = (
    { 
      create: onSubmit, 
      goBack: onCancel,
      productTypeOptions,
      providerOptions,
      initialValues
    }) => {

  return (
    <Container fluid>
      <Row>
        <h2>Nuevo Producto</h2>
      </Row>
      {!productTypeOptions.length ? (
        <Row>
          <Col>
            <Alert color="warning">
              No existen categorías. Click&nbsp;
              <Link to="../product-type/create">aquí</Link> para cargar nuevas
              categorías.
            </Alert>
          </Col>
        </Row>
        ) : 
        null}
      {!providerOptions.length ? (
        <Row>
          <Col>
            <Alert color="warning">
              No existen proveedores. Click&nbsp;
              <Link to="../provider/create">aquí</Link> para crear uno nuevo.
            </Alert>
          </Col>
        </Row>
        ) : 
        null}
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

Create.propTypes = {
  initialValues: PropTypes.object.isRequired,
  productTypeOptions: PropTypes.array.isRequired,
  providerOptions: PropTypes.array.isRequired,
  create: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
};

const mapStateToProps = state => {
  const productTypes = getProductTypes(state);
  const providers = getProviders(state);
  return {
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
    initialValues: {
      costPrice: 0.0,
      salePrice: 0.0,
      stock: 0,
      productTypeId: productTypes.length ? productTypes[0].id : "default",
      providerId: providers.length ? providers[0].id : "default"
    }
  };
};

const mapDispatchToProps = {
  create,
  goBack,
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Create);


