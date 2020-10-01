import React from "react";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import { create } from "../index";
import { getProductTypes } from "../../../productType/list";
import { getProviders } from "../../../providers/list";
import Form from "../../form/presentation";
import PropTypes from "prop-types";
import { Container, Row, Col } from "reactstrap";

const Create = ({ create: onSubmit, goBack: onCancel,  productTypeOptions, providerOptions, initialValues }) => {
  return (
    <Container fluid>
      <Row>
          <div className="block-header">
            <h2>Nuevo Producto</h2>
            </div>
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

Create.propTypes = {
  productTypeOptions: PropTypes.array.isRequired,
  providerOptions: PropTypes.array.isRequired,
  create: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
  initialValues: PropTypes.object.isRequired
};

const mapDispatchToProps = {
  create,
  goBack
};


const mapStateToProps = state => {
  const productTypes = getProductTypes(state);
  const providers = getProviders(state);
  return {
    productTypeOptions: productTypes.map(pt => ({
      label: pt.description,
      value: pt.id
    })),
    providerOptions: providers.map(provider => ({
      label: provider.name,
      value: provider.id
    })),
    initialValues: {
      productTypeId: productTypes.length ? productTypes[0].id : "default",
      providerId: providers.length ? providers[0].id : "default"
    }
  };
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Create);
