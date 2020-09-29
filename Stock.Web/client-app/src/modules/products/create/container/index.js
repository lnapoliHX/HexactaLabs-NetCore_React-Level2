import React from "react";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import { create } from "../index";
import Form from "../../form/presentation";
import PropTypes from "prop-types";
import { Container, Row, Col } from "reactstrap";
import { getProductTypes } from "../../../productType/list";
import { getProviders } from "../../../providers/list";

const Create = ({ create: onSubmit, goBack: onCancel, productTypeOptions, providerOptions, initialValues }) => {
  return (
    <Container fluid>
      <Row>
          <div className="block-header">
            <h2>Nuevo Producto</h2>
            </div>
      </Row>
      <Row>
        <Col>
          <Form onSubmit={onSubmit} handleCancel={onCancel} initialValues={initialValues} productTypeOptions={productTypeOptions} providerOptions={providerOptions}/>
        </Col>
      </Row>
    </Container>
  );
};

Create.propTypes = {
  create: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
  productTypeOptions: PropTypes.array.isRequired,
  providerOptions: PropTypes.array.isRequired,
  initialValues: PropTypes.object.isRequired
};

const mapDispatchToProps = {
  create,
  goBack
};

const mapStateToProps = state => {
  const productTypeOptions = getProductTypes(state).map(p => ({
    label: p.description,
    value: p.id
  }));
  const providerOptions = getProviders(state).map(p => ({
    label: p.name,
    value: p.id
  }));
  return {
    productTypeOptions: productTypeOptions,
    providerOptions: providerOptions,
    initialValues: {
      productTypeId: productTypeOptions.length ? productTypeOptions[0].value : "default",
      providerId: providerOptions.length ? providerOptions[0].value : "default"
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Create);
