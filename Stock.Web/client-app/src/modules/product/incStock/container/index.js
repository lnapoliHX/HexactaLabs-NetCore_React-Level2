import React from "react";
import { connect } from "react-redux";
import PropTypes from "prop-types";
import { getProductById } from "../../list";
import { increaseStock } from "..";
import Form from "../presentation";
import { Container, Row, Col } from "reactstrap";
import { goBack } from "connected-react-router";

const Stock = ({
  initialValues,
  increaseStock: onSubmit,
  goBack: onCancel,
}) => {
  return (
    <Container fluid>
      <Row>
        <Col>
          <div className="block-header">
            <h1>Manejo de stock</h1>
          </div>
        </Col>
      </Row>
      <Row>
        <Col>
          <Form
            initialValues={initialValues}
            onSubmit={onSubmit}
            handleCancel={onCancel}
          />
        </Col>
      </Row>
    </Container>
  );
};

Stock.propTypes = {
  initialValues: PropTypes.object.isRequired,
  increaseStock: PropTypes.func.isRequired,
  goBack: PropTypes.func.isRequired,
};

const mapStateToProps = (state, ownProps) => ({
  initialValues: getProductById(state, ownProps.match.params.id),
});

const mapDispatchToProps = {
  increaseStock,
  goBack,
};

export default connect(mapStateToProps, mapDispatchToProps)(Stock);
