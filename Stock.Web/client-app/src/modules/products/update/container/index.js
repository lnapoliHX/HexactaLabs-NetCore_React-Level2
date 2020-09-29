import React from "react";
import { connect } from "react-redux";
import { goBack } from "connected-react-router";
import PropTypes from "prop-types";
import { getProductById } from "../../list";
import { getProviders } from "../../../providers/list";
import { getProductTypes } from "../../../productType/list";
import { update } from "..";
import Form from "../../form/presentation";
import { Container, Row, Col } from "reactstrap";

const Update = ({ initialValues, update: onSubmit, goBack: onCancel, productTypeOptions, providerOptions }) => {
  return (
    <Container fluid>
      <Row>
          <Col>
            <div className="block-header">
                <h1>Edición</h1>
            </div>
        </Col>
      </Row>
      <Row>
        <Col>
          <Form
            initialValues={initialValues}
            onSubmit={onSubmit}
            handleCancel={onCancel}
            productTypeOptions={productTypeOptions}
            providerOptions={providerOptions}
          />
        </Col>
      </Row>
    </Container>
  );
};

Update.propTypes = {
  initialValues: PropTypes.object.isRequired,
  update: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
  productTypeOptions: PropTypes.array.isRequired,
  providerOptions: PropTypes.array.isRequired,
};

const mapStateToProps = (state, ownProps) => ({
  initialValues: getProductById(state, ownProps.match.params.id),
  productTypeOptions: getProductTypes(state).map(p => ({
    label: p.initials,
    value: p.id
  })),
  providerOptions: getProviders(state).map(p => ({
    label: p.name,
    value: p.id
  })),
});

const mapDispatchToProps = {
  update,
  goBack
};

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(Update);
